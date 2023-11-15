using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
   [SerializeField] float moveSpeed = 10f;
   [SerializeField] float upSpeed = 100f;
   [SerializeField] float rotateSpeed = 10f;
   [SerializeField] float waitTime = 2f;
   [SerializeField] GameObject upmove;
    public bool ShouldMove = true;
    public float maxY = 130f;           // 원하는 Y 축 최대 높이
    private bool canMoveUp = false;     // 위로 이동하는 상태와 벌칙자에게 이동하는 상태 구분하는 변수
    Animator animator;
    ParticleSystem Swim;
    GameObject punisher;

    void Start()
    {
        animator = GetComponent<Animator>();
        punisher = GameManager.Instance.Touchpoints[GameManager.Instance.PunisherIndex];//패배자 선정
        Swim = GetComponentInChildren<ParticleSystem>();
        upmove = GameObject.FindGameObjectWithTag("MainCamera");
        
    }

    void Update()
    {
        // 벌칙자에게 이동
        if (!canMoveUp)
        {
            //악어가 보아야할 방향을 targetRotation 로 지정
            Quaternion targetRotation = Quaternion.LookRotation(punisher.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            
            // 악어가 벌칙자 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, punisher.transform.position, moveSpeed * Time.deltaTime);

            // 악어가 벌칙자 위치에 가까워지면
            if (Vector3.Distance(transform.position, punisher.transform.position) < 0.2f)
            {
                canMoveUp = true;
            }
        }
        // 카메라 쪽으로 위로 이동 & 공격
        else
        {
            // 위로 이동한 상태면 공격
            if (transform.position.y >= maxY)
            {
                animator.SetTrigger("Attack");
            }
            // 카메라 쪽으로 위로 이동
            if (transform.position.y < maxY)
            {
                // 악어의 swim 파티클 해제
                Swim.gameObject.SetActive(false);

                Quaternion lookAt = Quaternion.identity;
                Vector3 lookatVec = (upmove.transform.position - this.transform.position).normalized;

                lookAt.SetLookRotation(lookatVec);
                transform.root.rotation = lookAt;

                transform.position = Vector3.Lerp(transform.position, upmove.transform.position, 0.01f);
                // 악어 크기 늘리기
                transform.localScale = new Vector3(40, 40, 40);
            }
        }
    }
    
    void OnAttackReady()
    {
        animator.SetFloat("AttackSpeed", 1f);
    }
    void OnAttack()
    {
        animator.SetFloat("AttackSpeed", 0.8f);
    }
   

}
