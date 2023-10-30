using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public float startWaitTime;
    public float rotate = 0.5f;
    public float respon = 5f;
    public Transform[] moveSpot;
    private int randomSpot;
    public bool IsTouch;
    public bool IsMove;
    Animator animator;
    
    private void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpot.Length);
        animator = GetComponent<Animator>();
        IsTouch = false;
        IsMove = false;
        

    }
    private void Update()
    {
        
        if(!IsTouch) //터치안할때
        {
            if(!IsMove)//움직이지 않을때
            {
                Move(); //움직임 활성화
                animator.SetBool("Sprint", true); // 애니메이션 움직임 활성화
               
                if (Vector3.Distance(transform.position, moveSpot[randomSpot].position) <= 0.2f) // 무브스팟과의 거리가 0.1f 보다 가까워지면
                {
                    //시간을감소, 웨이트 타임이 0보다 작아지면 다시 무빙상태 (그전은 아이들 상태)

                    
                    if (waitTime <= 0)
                    {
                        Debug.Log("이동활성화");
                        randomSpotMaking();//랜덤으로 가야할곳 설정
                        IsMove = false; //움직임 활성화
                        waitTime = Random.Range(0, 2f);
                        Debug.Log(waitTime);
                        startWaitTime = waitTime;
                    }
                    else
                    {
                        //움직임 비활성화
                        Debug.Log("이동 비활성화");
                        animator.SetBool("Sprint", false);
                        waitTime -= Time.deltaTime;
                    }


                   
                }
                else //무브스팟의 거리가 0.1 보다 멀면
                {
                    // 무빙상태유지
                    
                }
            }
            else //움직일때
            {
                //애니메이션 아이들 활성화
                //움직임 x
            }
        }
        

        
        else //터치 할때
        {
            //터치한 곳 위치를 받아서 저장한후 저장한곳으로 무빙
        }

    }
    private void Move() //움직임과 회전을 함
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpot[randomSpot].position, speed * Time.deltaTime);
        Vector3 dir = moveSpot[randomSpot].position - transform.position;
        Quaternion targetAngle = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, rotate * Time.deltaTime);

    }
    private void randomSpotMaking() // 랜덤한 위치 생성
    {
        randomSpot = Random.Range(0, moveSpot.Length);
    }
    



}
