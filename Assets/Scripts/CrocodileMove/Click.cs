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
        if (Input.GetMouseButtonDown(0)) //���콺 ���� ��ư Ŭ����
        {
            
            look.IsTouch = true; // �հ��� ��ġ�� true

            look.IsMove = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                destinationPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z); //����Ʈ�� ��ġ��  hit����Ʈ�� ���� ����, y���� �Ǿ��� ��ġ��

                ShoudMove = true; // �������� true �� �ٲ�
            }
        }
        if (ShoudMove) // �������� true �϶� ����
        {
            //�̵� �ִϸ��̼� ���
            animator.SetBool("Sprint", true);
            //���� �ִϸ��̼� ����
            animator.SetBool("Attack", false);
            //�Ǿ ���ƾ��� ������ targetRotation �� ����
            Quaternion targetRotation = Quaternion.LookRotation(destinationPoint - transform.position);
            //ȸ���� ������ �ϰ� targetRotation �������� �� 
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed*Time.deltaTime);
            //�������� ����Ʈ�� ��ġ�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, moveSpeed*Time.deltaTime);
            //���� ��Ȱ��ȭ
            ShouldAttack = false;
            //����Ʈ�� ��ġ�� 0.2f���� ����� ���� 
            if (Vector3.Distance(transform.position,destinationPoint)<= 0.2f)
            {
                ShoudMove = false; //������ ����
                animator.SetBool("Sprint", false);//�̵� �ִϸ��̼� ����
                ShouldAttack = true;//���� ����              
            }
            if (ShouldAttack)
            {
                StartCoroutine(attack()); //���� �ִϸ��̼� ���          
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
