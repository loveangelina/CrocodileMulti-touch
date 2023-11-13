using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] float startWaitTime = 2f;
    [SerializeField] float AroundSpeed = 10f;
    [SerializeField] float AroundAngleSpeed = 0.5f;
    [SerializeField] List<GameObject> touchpoints;
    Vector3 target;
    Animator animator;
    int randomPositionIndex;
    Vector3 randomOffset;
    Vector3 randomPosition;
    bool canMove = true;
    void Start()
    {
        waitTime = startWaitTime;
        animator = GetComponent<Animator>();
        touchpoints = GameManager.Instance.Touchpoints;

        SetRandomPosition();
    }

    void Update()
    {
        float dis = Vector3.Distance(transform.position, randomPosition);

        if (dis < 0.2f)
        {
            // 랜덤 포지션과 가까워지면 다시 새로운 랜덤 포지션 설정
            SetRandomPosition();
        }
        else
        {
            // 목표로 이동 
            Move();
        }
    }

    private void SetRandomPosition()
    {
        randomPositionIndex = Random.Range(0, touchpoints.Count);

        randomOffset = Random.onUnitSphere * 30f; //반지름 30f로 무작위 생성
        randomPosition = touchpoints[randomPositionIndex].transform.position + new Vector3(randomOffset.x, 0f, randomOffset.z); //랜덤한 위치 생성
        Debug.Log("랜덤한 터치포인트 : " + randomPosition);
    }

    public void Move()
    {
        // 랜덤한 터치포인트를 향한 방향벡터
        Vector3 direction = randomPosition - transform.position;

        // randomPosition을 향해 회전
        Quaternion AroundAngle = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, AroundAngle, AroundAngleSpeed * Time.deltaTime);

        // 랜덤한 터치포인트로 이동
        transform.position = Vector3.MoveTowards(transform.position, randomPosition, AroundSpeed * Time.deltaTime);
    }
}
