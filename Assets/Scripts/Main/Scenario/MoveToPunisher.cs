using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrocodile : ScenarioBase
{
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

    // 5초간 악어가 돌아다니다가 벌칙자 공격함 
    IEnumerator MoveToPunisher(ScenarioController controller)
    {
        Debug.Log("악어가 벌칙자에게 이동 시작");
        yield return new WaitForSeconds(5f);

        GameObject crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<RandMove>().enabled = false;
        crocodile.GetComponent<TouchScreen>().enabled = true;

        controller.SetNextScenario();
    }
}
