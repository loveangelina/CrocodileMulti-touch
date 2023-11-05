using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPunisher : ScenarioBase
{
    public override void Enter(ScenarioController controller)
    {
        GameObject crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<RandMove>().enabled = false;
        crocodile.GetComponent<TouchScreen>().enabled = true;
        Debug.Log("악어가 벌칙자에게 이동 시작");
    }

    public override void Execute(ScenarioController controller)
    {

    }

    public override void Exit()
    {

    }
}
