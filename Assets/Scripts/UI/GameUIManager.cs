using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{

    public GameObject pnlTouchAgain;
    public Text txtGameStart;
    public GameObject pnlBittenPay;

    private void Start()
    {
        //txtGameStart.gameObject.SetActive(true);
        //StartCoroutine(Countdown());
 
    }
    /*private void Update()
    {
        //TouchAgain 함수 잘 작동하는지 테스트용
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TouchAgain();
            
        }
    }*/

    public IEnumerator Countdown()
    {
        //게임씬 카운트다운 변수
        float countdownTime = 5f;

        txtGameStart.gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            txtGameStart.text = "TOUCH ALL OF THEM IN " + Mathf.Ceil(countdownTime) + " SECONDS !";
            countdownTime -= Time.deltaTime;
            yield return null;
        }

        txtGameStart.gameObject.SetActive(false);
    }

    public void TouchAgain()
    {
        pnlTouchAgain.SetActive(true); // Panel을 활성화

        // 2초 뒤에 비활성화
        StartCoroutine(DeactivatePanelAfterDelay(2f));
      
    }

    // 일정 시간이 지난 후 Panel을 비활성화하는 코루틴
    private IEnumerator DeactivatePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(2f); // 지정된 시간만큼 대기

        pnlTouchAgain.SetActive(false); // Panel을 비활성화

        // 2초가 지난 후 Countdown 함수 호출
        //StartCoroutine(Countdown());
    }

    public void DeActivePnl()
    {
        pnlTouchAgain.gameObject.SetActive(false);
    }

    public void BittenPay()
    {
        pnlBittenPay.gameObject.SetActive(true);
    }
}
