using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTouchpoints : MonoBehaviour
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
    [SerializeField] bool isTouchingCircle = false;

    void Start()
    {
        touchpointsLayer = LayerMask.GetMask("Effect");

        // 화면 크기 및 해상도 계산
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
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
                    // 터치 포인트를 화면 상의 비율로 변환
                    Vector3 viewport = new Vector3(touch.position.x / screenWidth, touch.position.y / screenHeight, 0f);

                    // 화면의 비율을 Unity 맵 상의 좌표로 변환
                    Vector3 touchPosition = new Vector3(
                        Mathf.Lerp(downLeft.position.x, upRight.position.x, viewport.x),
                        15f,
                        Mathf.Lerp(downLeft.position.z, upLeft.position.z, viewport.y)
                    );

                    if(debugMode)
                    {
                        Debug.Log("터치한 곳 viewport : " + viewport);
                        Debug.Log("터치한 곳 맵 상 위치 : " + touchPosition);
                    }

                    // 실제 터치한 곳과 터치포인트가 닿는지 찾기
                    Collider[] colliders = Physics.OverlapBox(touchPosition, boxSize, Quaternion.identity, touchpointsLayer);

                    if (colliders.Length == 0)
                    {
                        if (debugMode)
                        {
                            Debug.Log("안닿음");
                        }
                        // TODO : 다시 터치해달라고 재생성하러 돌아가야함 
                    }
                    else
                    {
                        // 정확성을 높히기 위해서 거리 체크 
                        foreach (Collider col in colliders)
                        {
                            Transform circle = col.GetComponent<Transform>();
                            float distance = Vector3.Distance(circle.position, touchPosition);
                            
                            if (debugMode)
                            {
                                Debug.Log("원 위치 : " + circle.position);
                                Debug.Log("사이 거리 : " + distance);
                            }
                                
                            if (distance < rad)
                            {
                                if (debugMode)
                                {
                                    Debug.Log("패스");
                                }
                            }
                            else
                            {
                                if (debugMode)
                                {
                                    Debug.Log("안닿음");
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
