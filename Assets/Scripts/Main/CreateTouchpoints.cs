using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTouchpoints : ScenarioBase
{
    [SerializeField] GameObject circlePrefab;
    private int numberOfParticipants = 5;   // TODO : �����ο� �� �޾ƿ���
    LayerMask obstacleLayer; // ��ֹ��� ���� ���̾�
    Vector3 boxSize = new Vector3(30f, 0.1f, 30f);
    [SerializeField] bool DebugMode;

    public override void Enter(ScenarioController controller)
    {
        obstacleLayer = LayerMask.GetMask("Terrain", "Effect");
        SpawnCircles();
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
        // ī�޶� ȭ�鿡 ��ġ�� x position ���� : -250f~150f
        float screenWidth = 400f;
        float pieceWidth = screenWidth / numberOfParticipants;

        for (int i = 0; i < numberOfParticipants; i++)
        {
            float randomX = Random.Range(-240f + i * pieceWidth, -240f + (i + 1) * pieceWidth);
            float randomZ = Random.Range(-220f, -10f);

            Vector3 spawnPosition = new Vector3(randomX, 15f, randomZ);

            // ��ֹ��� �浹���� �ʴ� ��ġ ã��
            Collider[] colliders = Physics.OverlapBox(spawnPosition, boxSize, Quaternion.identity, obstacleLayer);

            if (colliders.Length == 0)
            {
                Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                if (DebugMode)
                {
                    Debug.Log("i : " + i + " / �ٲ�� �� ��ġ" + spawnPosition);
                    foreach (Collider col in colliders)
                    {
                        Debug.Log(col.name);
                    }
                }

                i--;
            }
        }
    }

    void OnDrawGizmos()
    {
        /*
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(130, 15, -40), boxSize);
        */
    }
}
