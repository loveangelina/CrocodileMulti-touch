using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private List<GameObject> touchpoints;
    public int punisherIndex;

    public List<GameObject> Touchpoints
    {
        get { return touchpoints; }
        set { touchpoints = value; }
    }

    public int PunisherIndex
    {
        get { return punisherIndex; }
        set { punisherIndex = value; }
    }

    private void Awake()
    {
        // 인스턴스가 이미 존재하는 경우, 새로 생기는 인스턴스 삭제
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }

        // 인스턴스를 유일 오브젝트로 만듦
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void ClearTouchpoints()
    {
        if(touchpoints != null)
        {
            // 터치포인트 게임 오브젝트 삭제
            foreach(GameObject touchpoint in touchpoints)
            {
                Destroy(touchpoint);
            }

            // 카운트다운 UI 띄우는 것은 GameUIManager에서 해줌

            // 터치포인트 리스트 초기화
            touchpoints.Clear();
        }
    }

}
