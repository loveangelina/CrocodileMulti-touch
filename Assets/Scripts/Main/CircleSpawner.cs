using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{
    [SerializeField] GameObject circlePrefab;
    private int numberOfParticipants = 5;   
    LayerMask obstacleLayer; // ��ֹ��� ���� ���̾�
    Vector3 boxSize = new Vector3(30f, 0.1f, 30f);
    [SerializeField] bool DebugMode;
    List<GameObject> touchpoints = new List<GameObject>();

    void Start()
    {
        //numberOfParticipants = int.Parse(UIManager.Instance.txtSelect.text);
        obstacleLayer = LayerMask.GetMask("Terrain", "Effect");
        SpawnCircles();
    }

    void SpawnCircles()
    {
        // ī�޶� ȭ�鿡 ��ġ�� x position ���� : -230f~130f
        float screenWidth = 360f;
        float pieceWidth = screenWidth / numberOfParticipants;

        for (int i = 0; i < numberOfParticipants; i++)
        {
            float randomX = Random.Range(-230f + i * pieceWidth, -230f + (i + 1) * pieceWidth);
            float randomZ = Random.Range(-210f, -10f);

            Vector3 spawnPosition = new Vector3(randomX, 15f, randomZ);

            // ��ֹ��� �浹���� �ʴ� ��ġ ã��
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
                    Debug.Log("i : " + i + " / �ٲ�� �� ��ġ" + spawnPosition);
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

    void OnDrawGizmos()
    {
        /*
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(130, 15, -40), boxSize);
        */
    }
}
