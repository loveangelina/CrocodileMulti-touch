using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] float startWaitTime;
    [SerializeField] float AroundSpeed = 10f;
    [SerializeField] float AroundAngleSpeed = 0.5f;
    Vector3 target;
    Animator animator;
    int randomPositionIndex;
    Vector3 randomOffset;
    Vector3 randomPosition;
    bool canMove = false;
    RandomMove randomMove;
    void Start()
    {
        randomMove = new RandomMove();
        waitTime = startWaitTime;
        animator = GetComponent<Animator>();
        MakingAroundSpot();
        randomPositionIndex = Random.Range(0, GameManager.Instance.Touchpoints.Count);
        randomOffset = Random.onUnitSphere * 5f; //반지름 5f로 무작위 생성
        randomPosition = GameManager.Instance.Touchpoints[randomPositionIndex].transform.position + randomOffset; //랜덤한 위치 생성
    }

    void Update()
    {      
        if (canMove)
        {
            AroundMove();//회전과 움직임
            animator.SetBool("Sprint", true);
        }       
        float dis = Vector3.Distance(transform.position,randomPosition);
        if (dis < 0.2f)
        {
            if (waitTime <= 0)
            {
                MakingAroundSpot();//랜덤으로 가야할곳 설정
                canMove = true;
                waitTime = Random.Range(0, 2f);
                startWaitTime = waitTime;
            }
            else
            {
                canMove = false;
                animator.SetBool("Sprint", false);
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void MakingAroundSpot()
    {
        Debug.Log("랜덤포인트 위치 : " + randomPositionIndex);
        randomPositionIndex = Random.Range(0, GameManager.Instance.Touchpoints.Count); //터치포인트 리스트의 인덱스값 랜덤설정
    }

    public void AroundMove()
    {
        randomPositionIndex = Random.Range(0, GameManager.Instance.Touchpoints.Count);
        randomOffset = Random.onUnitSphere * 5f; //반지름 5f로 무작위 생성
        randomPosition = GameManager.Instance.Touchpoints[randomPositionIndex].transform.position + randomOffset; //랜덤한 위치 생성
        target = randomPosition - transform.position;
        //랜덤한 터치포인트를 향한 방향벡터

        //터치포인트 랜덤하게 돌기
        transform.position = Vector3.MoveTowards(transform.position,randomPosition,
        AroundSpeed * Time.deltaTime); //랜덤한 터치포인트로 이동

        //터치포인트를 향해 회전
        Quaternion AroundAngle = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, AroundAngle, AroundAngleSpeed * Time.deltaTime);
    }
    void DisableFirstScript()
    {
        RandMove firstScrpit = GetComponent<RandMove>();
        if (firstScrpit!=null)
        {
            firstScrpit.enabled = false;
        }
    }
}
