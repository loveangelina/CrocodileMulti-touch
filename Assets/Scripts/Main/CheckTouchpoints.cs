using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTouchpoints : MonoBehaviour
{
    LayerMask touchpointsLayer;
    Vector3 boxSize = new Vector3(30f, 0.1f, 30f);
    [SerializeField] float rad = 30f;

    [SerializeField] Transform downLeft;    // ������ (0, 0)
    [SerializeField] Transform upLeft;
    [SerializeField] Transform upRight;
    [SerializeField] float screenWidth;
    [SerializeField] float screenHeight;

    [SerializeField] bool debugMode = false;

    private Dictionary<GameObject, bool> touchPoints = new Dictionary<GameObject, bool>();

    void Start()
    {
        touchpointsLayer = LayerMask.GetMask("Effect");
        InitializeDictionary();

        // ȭ�� ũ�� �� �ػ� ���
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        // ��ġ �Է��� �߻����� ��
        if (Input.touchCount > 0)
        {
            // ��� ��ġ �Է¿� ���� �ݺ�
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // ��ġ �Է��� ���۵��� ��
                if (touch.phase == TouchPhase.Began)
                {
                    HandleTouchInput(touch);
                }
            }

            // �Էµ� ��ġ ������ �����ο� ���� ������
            if (Input.touchCount == 5)
            {
                if(debugMode)
                {
                    PrintDictionary(touchPoints);
                }

                if(AreAllCirclesTouched())
                {
                    Debug.Log("��Ģ�� ���� �ܰ�� �Ѿ");
                    // TODO : setNext();
                }
            }
        }
        else
        {
            // ���� ���� ���� ���� ������� ���ư�
            ClearDictionary();
        }
    }

    private void HandleTouchInput(Touch touch)
    {
        // ��ġ ����Ʈ�� ȭ�� ���� ������ ��ȯ
        Vector3 viewport = new Vector3(touch.position.x / screenWidth, touch.position.y / screenHeight, 0f);

        // ȭ���� ������ Unity �� ���� ��ǥ�� ��ȯ
        Vector3 touchPosition = new Vector3(
            Mathf.Lerp(downLeft.position.x, upRight.position.x, viewport.x),
            15f,
            Mathf.Lerp(downLeft.position.z, upLeft.position.z, viewport.y)
        );

        if (debugMode)
        {
            Debug.Log("��ġ�� �� viewport : " + viewport);
            Debug.Log("��ġ�� �� �� �� ��ġ : " + touchPosition);
        }

        // ���� ��ġ�� ���� ��ġ����Ʈ�� ����� ã��
        Collider[] colliders = Physics.OverlapBox(touchPosition, boxSize, Quaternion.identity, touchpointsLayer);

        if (colliders.Length == 1)
        {
            GameObject touchpoint = colliders[0].gameObject;
            
            // ���� ������ ��ġ�� �� ������
            if (!touchPoints[touchpoint])
            {
                touchPoints[touchpoint] = true;
            }
            else
            {
                if (debugMode)
                {
                    Debug.Log("�̹� ��ġ��");
                }
            }
        }
        else
        {
            // �� ���� ��ġ or ��ġ�� ���� ���� ���� ��ġ��
            if (debugMode)
            {
                Debug.Log("�ȴ���");
            }
        }
    }

    private bool AreAllCirclesTouched()
    {
        foreach (KeyValuePair<GameObject, bool> entry in touchPoints)
        {
            if (!entry.Value)
            {
                // false�ΰ� �ϳ��� ������ ��Ģ�� ���� �ܰ�� �Ѿ�� ����
                return false;
            }
        }
        return true;
    }

    void InitializeDictionary()
    {
        foreach(GameObject gameObject in GameManager.Instance.Touchpoints)
        {
            touchPoints.Add(gameObject, false);
        }
    }

    void ClearDictionary()
    {
        // ��ųʸ� ��� Ű�� ���� ����
        touchPoints.Clear();

        // ��ųʸ� �ʱ�ȭ
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
