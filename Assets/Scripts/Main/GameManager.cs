using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private List<GameObject> touchpoints;

    public List<GameObject> Touchpoints
    {
        get { return touchpoints; }
        set { touchpoints = value; }
    }

    private void Awake()
    {
        // �ν��Ͻ��� �̹� �����ϴ� ���, ���� ����� �ν��Ͻ� ����
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }

        // �ν��Ͻ��� ���� ������Ʈ�� ����
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
