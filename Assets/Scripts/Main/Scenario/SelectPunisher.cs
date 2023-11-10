using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectPunisher : ScenarioBase
{
    public override void Enter(ScenarioController controller)
    {
        int numberOfParticipants = UIManager.Instance.value;
        List<GameObject> touchpoints = GameManager.Instance.Touchpoints;

        int randomIndex = Random.Range(0, numberOfParticipants);
        GameManager.Instance.PunisherIndex = randomIndex;
        Debug.Log("벌칙자 인덱스 : " + GameManager.Instance.PunisherIndex);

        Vector3 selectedPosition = touchpoints[randomIndex].transform.position;
        Debug.Log("선택된 위치 : " + selectedPosition);

        controller.SetNextScenario();
    }

    public override void Execute(ScenarioController controller)
    {

    }

    public override void Exit()
    {

    }
}
