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
    public float maxY = 130f;           // ���ϴ� Y �� �ִ� ����
    private bool canMoveUp = false;     // ���� �̵��ϴ� ���¿� ��Ģ�ڿ��� �̵��ϴ� ���� �����ϴ� ����
    Animator animator;
    ParticleSystem Swim;
    GameObject punisher;

    void Start()
    {
        animator = GetComponent<Animator>();
        punisher = GameManager.Instance.Touchpoints[GameManager.Instance.PunisherIndex];//�й��� ����
        Swim = GetComponentInChildren<ParticleSystem>();
        upmove = GameObject.FindGameObjectWithTag("MainCamera");
        
    }

    void Update()
    {
        // ��Ģ�ڿ��� �̵�
        if (!canMoveUp)
        {
            //�Ǿ ���ƾ��� ������ targetRotation �� ����
            Quaternion targetRotation = Quaternion.LookRotation(punisher.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            
            // �Ǿ ��Ģ�� ��ġ�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, punisher.transform.position, moveSpeed * Time.deltaTime);

            // �Ǿ ��Ģ�� ��ġ�� ���������
            if (Vector3.Distance(transform.position, punisher.transform.position) < 0.2f)
            {
                canMoveUp = true;
            }
        }
        // ī�޶� ������ ���� �̵� & ����
        else
        {
            // ���� �̵��� ���¸� ����
            if (transform.position.y >= maxY)
            {
                animator.SetTrigger("Attack");
            }
            // ī�޶� ������ ���� �̵�
            if (transform.position.y < maxY)
            {
                // �Ǿ��� swim ��ƼŬ ����
                Swim.gameObject.SetActive(false);

                Quaternion lookAt = Quaternion.identity;
                Vector3 lookatVec = (upmove.transform.position - this.transform.position).normalized;

                lookAt.SetLookRotation(lookatVec);
                transform.root.rotation = lookAt;

                transform.position = Vector3.Lerp(transform.position, upmove.transform.position, 0.01f);
                // �Ǿ� ũ�� �ø���
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
