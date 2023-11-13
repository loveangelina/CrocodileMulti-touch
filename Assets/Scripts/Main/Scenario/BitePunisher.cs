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
        crocodile.GetComponent<TouchScreen>().enabled = true;

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
        // 악어가 벌칙자에게 이동하고 위로 올라오는 시간을 5초 줌
        yield return new WaitForSeconds(5f);

        gameUIManager = FindObjectOfType<GameUIManager>();
        gameUIManager.BittenPay();

        // TODO
        // 악어가 위로 올라온 후에 UI를 3초간 띄우도록 수정하기 
        // 악어의 attack -> sprint 상태 이용하기

        // 벌칙자가 선정되었다는 UI를 5초간 띄움
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Title");
    }
}
