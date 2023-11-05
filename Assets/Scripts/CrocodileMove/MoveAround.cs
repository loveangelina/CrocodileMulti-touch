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

    void Start()
    {
        waitTime = startWaitTime;
        animator = GetComponent<Animator>();
        MakingAroundSpot();
    }

    void Update()
    {
        AroundMove();//회전과 움직임
        animator.SetBool("Sprint", true);
        float dis = Vector3.Distance(transform.position, GameManager.Instance.Touchpoints[randomPositionIndex].transform.position);
        if (dis < 0.2f)
        {
            if (waitTime <= 0)
            {
                MakingAroundSpot();//랜덤으로 가야할곳 설정
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

    public void MakingAroundSpot()
    {
        Debug.Log("랜덤포인트 위치 : " + randomPositionIndex);
        randomPositionIndex = Random.Range(0, GameManager.Instance.Touchpoints.Count); //터치포인트 리스트의 인덱스값 랜덤설정
    }

    public void AroundMove()
    {
        target = GameManager.Instance.Touchpoints[randomPositionIndex].GetComponent<Transform>().position - transform.position;
        //랜덤한 터치포인트를 향한 방향벡터

        //터치포인트 랜덤하게 돌기
        transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.Touchpoints[randomPositionIndex].GetComponent<Transform>().position,
        AroundSpeed * Time.deltaTime); //랜덤한 터치포인트로 이동

        //터치포인트를 향해 회전
        Quaternion AroundAngle = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, AroundAngle, AroundAngleSpeed * Time.deltaTime);
    }
}
