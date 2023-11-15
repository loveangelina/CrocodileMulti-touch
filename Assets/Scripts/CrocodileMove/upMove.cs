using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class upMove : MonoBehaviour
{
    //공격 실험용 스크립트
    [SerializeField] GameObject upmove;
    Animator animator;
    public float maxY = 130f; // 원하는 Y 축 최대 높이
    ParticleSystem Swim;
    private bool canMoveUp = true;
    private bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        upmove = GameObject.FindGameObjectWithTag("MainCamera");
        animator = GetComponent<Animator>();
        Swim = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= maxY)
        {
            canMoveUp = false;
            if(canAttack)
            {
                StartCoroutine(Attack());
            }
            
        }
        if(transform.position.y <= maxY)
        {
            Swim.gameObject.SetActive(false);            
            StartCoroutine(MoveA());
        }
    }

    IEnumerator MoveA()
    {
        Quaternion lookAt = Quaternion.identity;
        Vector3 lookatVec = (upmove.transform.position - this.transform.position).normalized;
        
        lookAt.SetLookRotation(lookatVec);
        transform.rotation = lookAt;
        
        transform.position = Vector3.Lerp(transform.position, upmove.transform.position, 0.01f);       
        transform.localScale = new Vector3(25,25,25);
        yield return null;          
    }
    IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(0.8f);
        canAttack = false;
    }
    void OnAttackReady()
    {
        animator.SetFloat("AttackSpeed",1f);
    }
    void OnAttack()
    {
        animator.SetFloat("AttackSpeed", 0.8f);
    }
}
