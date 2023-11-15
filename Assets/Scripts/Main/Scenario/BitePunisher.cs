using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BitePunisher : ScenarioBase
{
    GameUIManager gameUIManager;
    GameObject crocodile;
    GameObject punisher;

    [SerializeField] float hSliderValueR = 0.0F;
    [SerializeField] float hSliderValueG = 0.0F;
    [SerializeField] float hSliderValueB = 0.0F;
    [SerializeField] float hSliderValueA = 1.0F;

    public override void Enter(ScenarioController controller)
    {
        crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<MoveAround>().enabled = false;
        crocodile.GetComponent<TouchScreen>().enabled = true;

        punisher = GameManager.Instance.Touchpoints[GameManager.Instance.PunisherIndex];
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
        ParticleSystem particle = punisher.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particle.main;

        // 악어가 위로 올라올 때 벌칙자 터치포인트에 효과 주기 시작
        while(crocodile.transform.position.y < 13f)
        {
            yield return null;
        }
        mainModule.startColor = new Color(1f, 0f, 0f);

        // 악어가 벌칙자에게 이동하고 위로 올라오는 시간을 5초 줌
        yield return new WaitForSeconds(5f);

        // 벌칙자가 선정되었다는 UI를 띄움
        //gameUIManager = FindObjectOfType<GameUIManager>();
        //gameUIManager.BittenPay();

        // 5초 후 타이틀씬으로 전환
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("End");
    }
}
