using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float upSpeed = 100f;

    public float rotateSpeed = 10f;

    public Vector3 destinationPoint;

    public float waitTime = 2f;
    public bool ShoudMove = false;
    public bool ShouldAttack = false;
    private MoveAround moveAround;

    Animator animator;
    ParticleSystem Swim;
    GameManager gameManager;
    GameObject punisher;
    // Start is called before the first frame update
    void Start()
    {
        moveAround = GetComponent<MoveAround>();
        animator = GetComponent<Animator>();
        punisher =GameManager.Instance.Touchpoints[GameManager.Instance.TouchpointIndex];//패배자 선정
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (ShoudMove) // 움직임이 true 일때 실행
        {
            moveAround.IsAround = false;//랜덤이동금지
            animator.SetBool("Sprint", true);
            //공격 애니메이션 멈춤
            animator.SetBool("Attack", false);
            //악어가 보아야할 방향을 targetRotation 로 지정
            Quaternion targetRotation = Quaternion.LookRotation(punisher.transform.position - transform.position);
            //회전을 스무스 하게 targetRotation 방향으로 함 
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed*Time.deltaTime);
            //움직임은 포인트의 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, punisher.transform.position, moveSpeed*Time.deltaTime);
            //공격 비활성화
            ShouldAttack = false;
            //포인트의 위치가 0.2f보다 가까워 지면 
            if (Vector3.Distance(transform.position, punisher.transform.position) <= 0.2f)
            {
                ShoudMove = false; //움직임 멈춤
                animator.SetBool("Sprint", false);//이동 애니메이션 멈춤
                ShouldAttack = true;//공격 가능              
            }
            if (ShouldAttack)
            {
                StartCoroutine(attack());
                //공격 애니메이션 재생          
                StartCoroutine(attackRotation());
                //공격할때 x 축으로 -90도 회전
            }
        }
    }
    IEnumerator attack()
    {
        animator.SetBool("Attack", true);

        yield return new WaitForSeconds(1.4f);

        animator.SetBool("Attack", false);
    }
    IEnumerator attackRotation()
    {
        Swim.Stop();
        transform.Rotate(new Vector3(-90,0,0)*0.8f);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 100f, transform.position.z), upSpeed);
        yield return new WaitForSeconds(1.4f); // 1초대기
        transform.position = new Vector3(transform.position.x, 12, transform.position.z);
        transform.Rotate(new Vector3(90, 0, 0) * 0.8f);
        Swim.Play();

    }
}
