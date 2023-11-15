using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrocodile : ScenarioBase
{
    [SerializeField] float moveTime = 15f;

    public override void Enter(ScenarioController controller)
    {
        Debug.Log("Move Crocodile");
        StartCoroutine(MoveToPunisher(controller));
    }

    public override void Execute(ScenarioController controller)
    {

    }

    public override void Exit()
    {

    }

    // 5�ʰ� �Ǿ ���ƴٴϴٰ� ��Ģ�� �������� �Ѿ
    IEnumerator MoveToPunisher(ScenarioController controller)
    {
        GameObject crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<RandMove>().enabled = false;
        crocodile.GetComponent<MoveAround>().enabled = true;

        Debug.Log("�Ǿ 5�ʰ� ��ġ����Ʈ ������ ��");
        yield return new WaitForSeconds(moveTime);

        controller.SetNextScenario();
    }
}
