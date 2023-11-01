using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public float moveSpeed = 10f;

    public float rotateSpeed = 10f;

    public Vector3 destinationPoint;

    public float waitTime = 2f;
    public bool ShoudMove = false;
    public bool ShouldAttack = false;
    private Look look;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        look = GetComponent<Look>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼 클릭시
        {
            
            look.IsTouch = true; // 손가락 터치가 true

            look.IsMove = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                destinationPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z); //포인트의 위치를  hit포인트로 새로 지정, y축은 악어의 위치로

                ShoudMove = true; // 움직임이 true 로 바뀜
            }
        }
        if (ShoudMove) // 움직임이 true 일때 실행
        {
            //이동 애니메이션 재생
            animator.SetBool("Sprint", true);
            //공격 애니메이션 멈춤
            animator.SetBool("Attack", false);
            //악어가 보아야할 방향을 targetRotation 로 지정
            Quaternion targetRotation = Quaternion.LookRotation(destinationPoint - transform.position);
            //회전을 스무스 하게 targetRotation 방향으로 함 
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed*Time.deltaTime);
            //움직임은 포인트의 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, moveSpeed*Time.deltaTime);
            //공격 비활성화
            ShouldAttack = false;
            //포인트의 위치가 0.2f보다 가까워 지면 
            if (Vector3.Distance(transform.position,destinationPoint)<= 0.2f)
            {
                ShoudMove = false; //움직임 멈춤
                animator.SetBool("Sprint", false);//이동 애니메이션 멈춤
                ShouldAttack = true;//공격 가능              
            }
            if (ShouldAttack)
            {
                StartCoroutine(attack()); //공격 애니메이션 재생          
            }
        }
    }
    IEnumerator attack()
    {
        animator.SetBool("Attack", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("Attack", false);
    }
}
