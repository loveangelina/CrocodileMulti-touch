using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndingSceneManager : MonoBehaviour
{
    public Text txtWait;

    void Start()
    {
        StartCoroutine(Countdown());
        // 5초 후에 타이틀 씬으로 전환
        Invoke("LoadTitleScene", 5f);
    }

    // 타이틀 씬으로 전환하는 메서드
    void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
        SoundManager.Instance.SwapBGMClip(0);
        UIManager.Instance.value = 2;
    }

    public IEnumerator Countdown()
    {
        //게임씬 카운트다운 변수
        float countdownTime = 5f;

        txtWait.gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            txtWait.text = "PLEASE WAIT FOR " + Mathf.Ceil(countdownTime) + " SECONDS...";
            countdownTime -= Time.deltaTime;
            yield return null;
        }

        txtWait.gameObject.SetActive(false);
    }
}
