using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioController : MonoBehaviour
{
	[SerializeField]
	private	List<ScenarioBase>	scenarios;
	[SerializeField]
	private	string				nextSceneName = "";

	private ScenarioBase		currentScenario = null;
	private	int					currentIndex = -1;

	private void Start()
	{
		SetNextScenario();
		Debug.Log(currentIndex);
	}

	private void Update()
	{
		if (currentScenario != null )
		{
			currentScenario.Execute(this);
		}
	}

	public void SetNextScenario(int index = -5)
	{
		// 현재 시나리오의 Exit() 메소드 호출
		if (currentScenario != null )
		{
			currentScenario.Exit();
		}

		if(index != -5)
        {
			currentIndex = index;
		}

		// 마지막 시나리오를 진행했다면 CompletedAllTutorials() 메소드 호출
		if ( currentIndex >= scenarios.Count-1 )
		{
			CompletedAllScenarios();
			return;
		}

		// 다음 시나리오 과정을 currentTutorial로 등록
		currentIndex ++;
		currentScenario = scenarios[currentIndex];

		// 새로 바뀐 시나리오의 Enter() 메소드 호출
		currentScenario.Enter(this);
	}

	public void CompletedAllScenarios()
	{
		currentScenario = null;

		// 행동 양식이 여러 종류가 되었을 때 코드 추가 작성
		// 현재는 씬 전환

		Debug.Log("Complete All");

		if ( !nextSceneName.Equals("") )
		{
			SceneManager.LoadScene(nextSceneName);
		}
	}
}

