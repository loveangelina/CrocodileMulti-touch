using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BitePunisher : ScenarioBase
{
    GameUIManager gameUIManager;

    public override void Enter(ScenarioController controller)
    {
        GameObject crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<MoveAround>().enabled = false;
        crocodile.GetComponent<upMove>().enabled = true;

        // 벌칙자 선정 UI 활성화
        StartCoroutine(ShowPunisherUI());

        //controller.SetNextScenario();
    }

    public override void Execute(ScenarioController controller)
    {
    }

    public override void Exit()
    {
    }

    IEnumerator ShowPunisherUI()
    {
        yield return new WaitForSeconds(2f);

        gameUIManager = FindObjectOfType<GameUIManager>();
        gameUIManager.BittenPay();

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Title");
    }
}
