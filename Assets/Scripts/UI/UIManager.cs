using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region 싱글턴 인스턴스
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<UIManager>();
                }
            }
            return _instance;
        }
    }
    

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion



    //버튼 연결 변수

    public Button btnSetting;
    public Button btnExit;
    public Button btnMin;
    public Button btnMax;
    public Button btnGameStart;

    public GameObject pnlSetting;
    public Text txtSelect;

    //인원 선택 변수
    private int value = 1;

    //셋팅 창 관련 변수
    public Slider sldSoundFxVolume;
    public Slider sldBGMVolume;
  
    public Toggle toggleSoundFxMute;
    public Toggle toggleBGMMute;


    private bool isSoundFxMuted = false;
    private bool isBGMMuted = false;


    private void Start()
    {
        btnExit.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            OnClickExit();
        });
        btnSetting.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            OnClickToggleSetting();
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


        //사운드
        sldBGMVolume.onValueChanged.AddListener(SetBGMVolume);
        sldSoundFxVolume.onValueChanged.AddListener(SetSoundFxVolume);
        toggleBGMMute.onValueChanged.AddListener(ToggleBGMMute);
        toggleSoundFxMute.onValueChanged.AddListener(ToggleSoundFxMute);
    }

    #region TITLE 씬 버튼 기능 함수
    //게임 종료
    public void OnClickExit()
    {
        Application.Quit();
    }
    //Setting 창 활성화/비활성화
    public void OnClickToggleSetting()
    {
        pnlSetting.SetActive(!pnlSetting.activeSelf);
    }

    //
    public void OnClickIncrease()
    {
        value = int.Parse(txtSelect.text);
        if (value < 5)
        {
            txtSelect.text = (value + 1).ToString();
        }
    }

    public void OnClickDecrease()
    {
        value = int.Parse(txtSelect.text);
        if (value > 1)
        {
            txtSelect.text = (value - 1).ToString();
        }
    }
    //게임 시작
    public void OnClickGameStart()
    {
        SceneManager.LoadScene("Game"); 
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

