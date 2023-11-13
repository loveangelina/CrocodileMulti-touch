using UnityEngine;

public class RandMove : MonoBehaviour
{
   [SerializeField] float speed;
   [SerializeField] float waitTime;
   [SerializeField] float startWaitTime;
   [SerializeField] float rotate = 0.5f;
   [SerializeField] float attackRotate = 0.5f;
   [SerializeField] float respon = 5f;
   [SerializeField] Transform[] moveSpots;
   int randomSpot;
   Animator animator;

    private void Start()
    {
        waitTime = startWaitTime;
        randomSpotMaking();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move(); //움직임 활성화

        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) <= 0.2f) // 무브스팟과의 거리가 0.1f 보다 가까워지면
        {
            //시간을감소, 웨이트 타임이 0보다 작아지면 다시 무빙상태 (그전은 아이들 상태)                  
            if (waitTime <= 0)
            {                       
                randomSpotMaking();//랜덤으로 가야할곳 설정
                waitTime = Random.Range(0, 2f);                      
                startWaitTime = waitTime;
            }
            else
            {                       
                waitTime -= Time.deltaTime;
            }
        }               
    }
    private void Move() //움직임과 회전을 함
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        Vector3 dir = moveSpots[randomSpot].position - transform.position;
        if(dir != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, rotate * Time.deltaTime);
        }
    }

    private void randomSpotMaking() // 랜덤한 위치 생성
    {
        randomSpot = Random.Range(0, moveSpots.Length);
    }

}
