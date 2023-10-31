using UnityEngine;

public abstract class ScenarioBase : MonoBehaviour
{
	// 해당 과정을 시작할 때 1회 호출
	public abstract void Enter(ScenarioController controller);

	// 해당 과정을 진행하는 동안 매 프레임 호출
	public abstract void Execute(ScenarioController controller);
	
	// 해당 과정을 종료할 때 1회 호출
	public abstract void Exit();
}


