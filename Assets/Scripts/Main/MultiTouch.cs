using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiTouch : MonoBehaviour
{
    void Start()
    {
        // starts after all participants touch.
        StartCoroutine(SelectPunisher());
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

    IEnumerator SelectPunisher()
    {
        // TODO : edit this seconds
        yield return new WaitForSeconds(2f);
        int touchCount = Input.touchCount;
        Vector2[] touchPositions = GetTouchPoints(touchCount);

        if (touchCount > 0)
        {
            int randomIndex = Random.Range(0, touchCount);
            Vector2 selectedPosition = touchPositions[randomIndex];
            Debug.Log("선택된 위치 : " + selectedPosition);
        }
    }
}
