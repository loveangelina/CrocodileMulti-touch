using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] float startWaitTime;
    public bool IsAround;
    [SerializeField] float AroundSpeed = 10f;
    [SerializeField] float AroundAngleSpeed = 0.5f;
    Vector3 target;
    Animator animator;
    RandMove randMove;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        animator = GetComponent<Animator>();
        randMove = GetComponent<RandMove>();
        GameManager.Instance.touchpointIndex = Random.Range(0, GameManager.Instance.Touchpoints.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
       if (IsAround)//주위를 돌고 있을때
        {
            randMove.IsTouch = true;
            AroundMove();//회전과 움직임
            animator.SetBool("Sprint", true);
            if (Vector3.Distance(transform.position, GameManager.Instance.Touchpoints[GameManager.Instance.touchpointIndex].GetComponent<Transform>().position) < 0.2f)
            {                
                if (waitTime <= 0)
                {
                    MakingAroundSpot();//랜덤으로 가야할곳 설정
                    IsAround = true; //움직임 활성화
                    waitTime = Random.Range(0, 2f);
                    startWaitTime = waitTime;
                }
                else
                { 
                    animator.SetBool("Sprint", false);
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
    public void MakingAroundSpot()
    {
        GameManager.Instance.touchpointIndex = Random.Range(0, GameManager.Instance.Touchpoints.Count); //터치포인트 리스트의 인덱스값 랜덤설정
    }
    public void AroundMove()
    {
        target = GameManager.Instance.Touchpoints[GameManager.Instance.touchpointIndex].GetComponent<Transform>().position - transform.position;
        //랜덤한 터치포인트를 향한 방향벡터

        //터치포인트 랜덤하게 돌기
        transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.Touchpoints[GameManager.Instance.touchpointIndex].GetComponent<Transform>().position,
        AroundSpeed * Time.deltaTime); //랜덤한 터치포인트로 이동

        //터치포인트를 향해 회전
        Quaternion AroundAngle = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, AroundAngle, AroundAngleSpeed * Time.deltaTime);
    }
}
