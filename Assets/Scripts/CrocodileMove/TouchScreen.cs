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
    public float maxY = 130f; // 원하는 Y 축 최대 높이
    private bool canMove = true;
    private bool canMoveUp = true;
    private bool canAttack = true;
    Animator animator;
    ParticleSystem Swim;
    GameObject punisher;

    void Start()
    {
        animator = GetComponent<Animator>();
        punisher =GameManager.Instance.Touchpoints[GameManager.Instance.PunisherIndex];//패배자 선정
        Swim = GetComponentInChildren<ParticleSystem>();
        upmove = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (ShouldMove) // 움직임이 true 일때 실행
        {
            if(canMove)
            {
                animator.SetBool("Sprint", true);
                //악어가 보아야할 방향을 targetRotation 로 지정
                Quaternion targetRotation = Quaternion.LookRotation(punisher.transform.position - transform.position);
                //회전을 스무스 하게 targetRotation 방향으로 함 
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                //움직임은 포인트의 위치로 이동
                transform.position = Vector3.MoveTowards(transform.position, punisher.transform.position, moveSpeed * Time.deltaTime);              
            }                     
            //포인트의 위치가 0.2f보다 가까워 지면 
            if (Vector3.Distance(transform.position, punisher.transform.position) <= 0.2f)
            {
                canMove = false; //좌우 이동 금지
                animator.SetBool("Sprint", false);//이동 애니메이션 멈춤
                if (transform.position.y >= maxY)
                {
                    canMoveUp = false;
                    if (canAttack)
                    {
                        StartCoroutine(Attack());
                    }
                }
                if(transform.position.y <= maxY)
                {
                    Swim.gameObject.SetActive(false);
                    StartCoroutine(Move());
                }
            }
        }        
    }
    
    IEnumerator Move()
    {
        Quaternion lookAt = Quaternion.identity;
        Vector3 lookatVec = (upmove.transform.position - this.transform.position).normalized;

        lookAt.SetLookRotation(lookatVec);
        transform.root.rotation = lookAt;

        transform.position = Vector3.Lerp(transform.position, upmove.transform.position, 0.01f);
        transform.localScale = new Vector3(25, 25, 25);
        yield return null;

    }
    IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        Handheld.Vibrate();//진동주기
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(0.8f);
        canAttack = false;
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
