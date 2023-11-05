using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitePunisher : ScenarioBase
{
    public override void Enter(ScenarioController controller)
    {
        GameObject crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<MoveAround>().enabled = false;
        crocodile.GetComponent<TouchScreen>().enabled = true;
        crocodile.GetComponent<TouchScreen>().ShouldMove = true;

        //controller.SetNextScenario();
    }

    public override void Execute(ScenarioController controller)
    {
    }

    public override void Exit()
    {
    }
}
