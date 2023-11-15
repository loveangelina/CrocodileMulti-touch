using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTouchpoints : ScenarioBase
{
    [SerializeField] GameObject circlePrefab;
    private int numberOfParticipants = 5;
    LayerMask obstacleLayer; // 장애물로 삼을 레이어
    Vector3 boxSize = new Vector3(30f, 0.1f, 30f);
    [SerializeField] bool DebugMode;
    List<GameObject> touchpoints = new List<GameObject>();

    public override void Enter(ScenarioController controller)
    {
        // 터치포인트 초기화
        GameManager.Instance.ClearTouchpoints();

        // 악어 RandMove 활성화 (랜덤 움직임)
        GameObject crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<RandMove>().enabled = true;

        numberOfParticipants = UIManager.Instance.value;
        obstacleLayer = LayerMask.GetMask("Terrain", "Effect");
        SpawnCircles();

        // 터치하라는 UI 활성화
        GameUIManager uiManager = FindObjectOfType<GameUIManager>();
        StartCoroutine(uiManager.Countdown());

        controller.SetNextScenario();
    }

    public override void Execute(ScenarioController controller)
    {

    }

    public override void Exit()
    {

    }

    void SpawnCircles()
    {
        // 카메라 화면에 비치는 x position 범위 : -230f~130f
        float screenWidth = 360f;
        float pieceWidth = screenWidth / numberOfParticipants;

        for (int i = 0; i < numberOfParticipants; i++)
        {
            float randomX = Random.Range(-230f + i * pieceWidth, -230f + (i + 1) * pieceWidth);
            float randomZ = Random.Range(-210f, -10f);

            Vector3 spawnPosition = new Vector3(randomX, 15f, randomZ);

            // 장애물과 충돌하지 않는 위치 찾기
            Collider[] colliders = Physics.OverlapBox(spawnPosition, boxSize, Quaternion.identity, obstacleLayer);

            if (colliders.Length == 0)
            {
                GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
                touchpoints.Add(circle);
            }
            else
            {
                if (DebugMode)
                {
                    Debug.Log("i : " + i + " / 바뀌기 전 위치" + spawnPosition);
                    foreach (Collider col in colliders)
                    {
                        Debug.Log(col.name);
                    }
                }

                i--;
            }
        }

        GameManager.Instance.Touchpoints = touchpoints;
    }
}
