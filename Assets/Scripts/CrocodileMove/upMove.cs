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
    public UnityEvent StopMove;
    // Start is called before the first frame update
    void Start()
    {
        upmove = GameObject.FindGameObjectWithTag("MainCamera");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopMove.Invoke();
            StartCoroutine(MoveA());
        }
    }

    IEnumerator MoveA()
    {
        
        transform.Rotate(new Vector3(-90,0,0)*0.8f);        
        transform.position = Vector3.MoveTowards(transform.position, upmove.transform.position, 120f);
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(0.8f);       
        transform.position = new Vector3(transform.position.x, 12, transform.position.z);
        transform.Rotate(new Vector3(90, 0, 0) * 0.8f);

    }
}
