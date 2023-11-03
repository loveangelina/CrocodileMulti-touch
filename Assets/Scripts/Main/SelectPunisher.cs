using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectPunisher : MonoBehaviour
{
    void Start()
    {
        // starts after all participants touch.
        StartCoroutine(GetPositionOfPunisher());
    }

    private Vector2[] GetTouchPoints(int touchCount)
    {
        Vector2[] touchPositions = new Vector2[touchCount];

        for(int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            touchPositions[i] = touch.position;
            Debug.Log("위치 : " + touchPositions[i]);
        }

        return touchPositions;
    }

    IEnumerator GetPositionOfPunisher()
    {
        // TODO : edit this seconds
        yield return new WaitForSeconds(2f);
        int numberOfParticipants = int.Parse(UIManager.Instance.txtSelect.text);
        List<GameObject> touchpoints = GameManager.Instance.Touchpoints;

        int randomIndex = Random.Range(0, numberOfParticipants);
        Vector3 selectedPosition = touchpoints[randomIndex].transform.position;
        Debug.Log("선택된 위치 : " + selectedPosition);
    }
}
