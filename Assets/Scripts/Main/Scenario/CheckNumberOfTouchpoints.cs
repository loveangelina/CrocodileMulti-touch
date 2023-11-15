using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNumberOfTouchpoints : ScenarioBase
{
    LayerMask touchpointsLayer;
    Vector3 boxSize = new Vector3(30f, 0.1f, 30f);

    [SerializeField] float rad = 30f;
    [SerializeField] Transform downLeft;    // 기준점 (0, 0)
    [SerializeField] Transform upLeft;
    [SerializeField] Transform upRight;
    [SerializeField] float screenWidth;
    [SerializeField] float screenHeight;
    [SerializeField] bool debugMode = false;

    private Dictionary<GameObject, bool> touchPoints = new Dictionary<GameObject, bool>();

    private float timer = 5f; // 시간 제한 설정 (5초)

    GameUIManager gameUIManager;

    public override void Enter(ScenarioController controller)
    {
        touchpointsLayer = LayerMask.GetMask("Effect");
        InitializeDictionary();

        gameUIManager = FindObjectOfType<GameUIManager>();

        // 화면 크기 및 해상도 계산
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        StartCoroutine(StartTimer(controller));
    }

    public override void Execute(ScenarioController controller)
    {
        // 터치 입력이 발생했을 때
        if (Input.touchCount > 0)
        {
            // 모든 터치 입력에 대해 반복
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // 터치 입력이 시작됐을 때
                if (touch.phase == TouchPhase.Began)
                {
                    HandleTouchInput(touch);
                }
            }

            // 입력된 터치 개수가 참여인원 수와 같으면
            if (Input.touchCount == UIManager.Instance.value)
            {
                if (debugMode)
                {
                    PrintDictionary(touchPoints);
                }

                if (AreAllCirclesTouched())
                {
                    Debug.Log("벌칙자 선정 단계로 넘어감");
                    controller.SetNextScenario();
                }
            }
        }
        else
        {
            // 손을 떼면 사전 값이 원래대로 돌아감
            ClearDictionary();
        }
    }

    public override void Exit()
    {

    }

    private void HandleTouchInput(Touch touch)
    {
        // 터치 포인트를 화면 상의 비율로 변환
        Vector3 viewport = new Vector3(touch.position.x / screenWidth, touch.position.y / screenHeight, 0f);

        // 화면의 비율을 Unity 맵 상의 좌표로 변환
        Vector3 touchPosition = new Vector3(
            Mathf.Lerp(downLeft.position.x, upRight.position.x, viewport.x),
            15f,
            Mathf.Lerp(downLeft.position.z, upLeft.position.z, viewport.y)
        );

        if (debugMode)
        {
            Debug.Log("터치한 곳 viewport : " + viewport);
            Debug.Log("터치한 곳 맵 상 위치 : " + touchPosition);
        }

        // 실제 터치한 곳과 터치포인트가 닿는지 찾기
        Collider[] colliders = Physics.OverlapBox(touchPosition, boxSize, Quaternion.identity, touchpointsLayer);

        if (colliders.Length == 1)
        {
            GameObject touchpoint = colliders[0].gameObject;

            // 원이 이전에 터치된 적 없으면
            if (!touchPoints[touchpoint])
            {
                touchPoints[touchpoint] = true;
            }
            else
            {
                if (debugMode)
                {
                    Debug.Log("이미 터치됨");
                }
            }
        }
        else
        {
            // 원 밖을 터치 or 터치한 곳이 여러 원과 겹치면
            if (debugMode)
            {
                Debug.Log("안닿음");
            }
        }
    }

    private bool AreAllCirclesTouched()
    {
        foreach (KeyValuePair<GameObject, bool> entry in touchPoints)
        {
            if (!entry.Value)
            {
                // false인게 하나라도 있으면 벌칙자 선정 단계로 넘어가지 못함
                return false;
            }
        }
        return true;
    }

    IEnumerator StartTimer(ScenarioController controller)
    {
        yield return new WaitForSeconds(timer);

        // 시간 내에 모든 터치포인트가 클릭되지 않았을 때
        if (!AreAllCirclesTouched())
        {
            // 오류 메세지 UI 띄우고 카운트다운 UI 띄움
            gameUIManager.TouchAgain();
            yield return new WaitForSeconds(2f);

            controller.SetNextScenario(-1);
            Debug.Log("터치포인트 재생성");
        }
    }

    void InitializeDictionary()
    {
        foreach (GameObject gameObject in GameManager.Instance.Touchpoints)
        {
            touchPoints.Add(gameObject, false);
        }
    }

    void ClearDictionary()
    {
        // 딕셔너리 모든 키와 값을 제거
        touchPoints.Clear();

        // 딕셔너리 초기화
        InitializeDictionary();
    }

    private static void PrintDictionary(Dictionary<GameObject, bool> dict)
    {
        foreach (KeyValuePair<GameObject, bool> entry in dict)
        {
            Debug.Log("Key: " + entry.Key.transform.position +
                        " / value : " + entry.Value);
        }
    }
}
