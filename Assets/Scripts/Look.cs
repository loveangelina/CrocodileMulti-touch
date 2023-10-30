using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public float rotate = 0.5f;

    public Transform[] moveSpot;
    private int randomSpot;
    bool IsMoving;

    Animator animator;
    private void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpot.Length);
        animator = GetComponent<Animator>();
        IsMoving = true;

    }
    private void Update()
    {
        if(IsMoving)
        {
            animator.SetBool("Sprint", true);
            Move();
        }
        

        if (Vector3.Distance(transform.position, moveSpot[randomSpot].position) < 0.1f)
        {
           
            if (waitTime <= 0)
            {
                IsMoving = true;
                randomSpotMaking();
                waitTime = startWaitTime;
            }
            else
            {
                IsMoving = false;
                animator.SetBool("Sprint", false);
                waitTime -= Time.deltaTime;
            }
        }

    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpot[randomSpot].position, speed * Time.deltaTime);
        Vector3 dir = moveSpot[randomSpot].position - transform.position;
        Quaternion targetAngle = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, rotate * Time.deltaTime);
    }
    private void randomSpotMaking()
    {
        randomSpot = Random.Range(0, moveSpot.Length);
    }




}
