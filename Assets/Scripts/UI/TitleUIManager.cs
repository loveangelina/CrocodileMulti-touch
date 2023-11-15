using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleUIManager : MonoBehaviour
{
   

    //버튼 연결 변수

    public Button btnSetting;
    public Button btnExit;
    public Button btnMin;
    public Button btnMax;
    public Button btnGameStart;
    public Button btnExitset;

    public GameObject pnlSetting;
    public Text txtSelect;

    //셋팅 창 관련 변수
    public Slider sldSoundFxVolume;
    public Slider sldBGMVolume;

    public Toggle toggleSoundFxMute;
    public Toggle toggleBGMMute;


    private bool isSoundFxMuted = false;
    private bool isBGMMuted = false;



    #region AddListner 연결
    private void Start()
    {
        //버튼 AddListner
        btnExit.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            UIManager.Instance.OnClickExit();
        });
        btnSetting.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            UIManager.Instance.ActivePnl(pnlSetting);
        });
        btnMin.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            OnClickDecrease();
        });
        btnMax.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            OnClickIncrease();
        });
        btnGameStart.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayGameStartSound();
            OnClickGameStart();
        });
        btnExitset.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            UIManager.Instance.ActivePnl(pnlSetting);
        });


        //사운드
        sldBGMVolume.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetBGMVolume(value);

        });

        sldSoundFxVolume.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetSoundFxVolume(value);

        });

        toggleBGMMute.onValueChanged.AddListener((isMuted) =>
        {
            SoundManager.Instance.MuteBGM(isMuted);

        });

        toggleSoundFxMute.onValueChanged.AddListener((isMuted) =>
        {
            SoundManager.Instance.MuteSoundFx(isMuted);

        });
    }

    #endregion

    #region TItle 씬 버튼 기능 함수
    // Increase 버튼 클릭시 value 값 1씩 증가, 최대 5
    public void OnClickIncrease()
    {
        UIManager.Instance.value = int.Parse(txtSelect.text);

        if (UIManager.Instance.value < 5)
        {
            UIManager.Instance.value += 1;
            txtSelect.text = UIManager.Instance.value.ToString();
        }

       
    }

    // Decrease 버튼 클릭시 value 값 1씩 감소, 최소 2
    public void OnClickDecrease()
    {
        UIManager.Instance.value = int.Parse(txtSelect.text);

        if (UIManager.Instance.value > 2)
        {
            UIManager.Instance.value -= 1;
            txtSelect.text = UIManager.Instance.value.ToString();
        }
    }

    public void OnClickGameStart()
    {
        SceneManager.LoadScene("LakeScene");
        SoundManager.Instance.SwapBGMClip(1);
    }
    #endregion

    #region SETTING 창 기능 함수

    //볼륨 조절 함수
    public void SetSoundFxVolume(float volume)
    {

        SoundManager.Instance.SetSoundFxVolume(volume);

        if (volume <= 0)
        {
            isSoundFxMuted = true;
            toggleSoundFxMute.isOn = true;
        }
        else
        {
            isSoundFxMuted = false;
            toggleSoundFxMute.isOn = false;
        }
    }

    public void SetBGMVolume(float volume)
    {
        Debug.Log("Setting BGM volume to: " + volume);
        SoundManager.Instance.SetBGMVolume(volume);

        if (volume <= 0)
        {
            isBGMMuted = true;
            toggleBGMMute.isOn = true;
        }
        else
        {
            isBGMMuted = false;
            toggleBGMMute.isOn = false;
        }
    }

    //음소거 함수
    public void ToggleBGMMute(bool isMuted)
    {
        isBGMMuted = isMuted;

        // SoundManager의 음소거 기능 호출
        SoundManager.Instance.MuteBGM(isBGMMuted);
    }

    public void ToggleSoundFxMute(bool isMuted)
    {
        isSoundFxMuted = isMuted;

        // SoundManager의 음소거 기능 호출
        SoundManager.Instance.MuteSoundFx(isSoundFxMuted);

    }
    #endregion


}

