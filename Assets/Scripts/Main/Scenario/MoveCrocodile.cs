using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrocodile : ScenarioBase
{
    [SerializeField] float moveTime = 5f;

    public override void Enter(ScenarioController controller)
    {
        StartCoroutine(MoveToPunisher(controller));
    }

    public override void Execute(ScenarioController controller)
    {

    }

    public override void Exit()
    {

    }

    // 5초간 악어가 돌아다니다가 벌칙자 공격으로 넘어감
    IEnumerator MoveToPunisher(ScenarioController controller)
    {
        GameObject crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<RandMove>().enabled = false;
        crocodile.GetComponent<MoveAround>().enabled = true;

        Debug.Log("악어가 5초간 터치포인트 주위를 돔");
        yield return new WaitForSeconds(moveTime);

        controller.SetNextScenario();
    }
}
