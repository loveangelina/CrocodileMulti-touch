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

    #region 변수들
    //버튼 연결 변수

    public Button btnSetting;
    public Button btnExit;
    public Button btnMin;
    public Button btnMax;
    public Button btnGameStart;
    public Button btnExitset;

 
    public GameObject pnlSetting;
    public GameObject pnlTouchagain;
    public Text txtSelect;
    public Text txtGameStart;

    //인원 선택 변수
    public int value = 2;

    //셋팅 창 관련 변수
    public Slider sldSoundFxVolume;
    public Slider sldBGMVolume;

    public Toggle toggleSoundFxMute;
    public Toggle toggleBGMMute;


    private bool isSoundFxMuted = false;
    private bool isBGMMuted = false;


    public Canvas TitleUIcanvas;
    public Canvas GameUIcanvas;

    float countdownTime = 5f;

    #endregion

    #region AddListner 연결
    private void Start()
        {
            //버튼 AddListner
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
        btnExitset.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayButtonClickSound();
            OnClickToggleSetting();
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

    // Increase 버튼 클릭시 value 값 1씩 증가, 최대 5
    public void OnClickIncrease()
    {
        value = int.Parse(txtSelect.text);

        if (value < 5)
        {
            value += 1;
            txtSelect.text = value.ToString();
        }

        Debug.Log(value);
    }

    // Decrease 버튼 클릭시 value 값 1씩 감소, 최소 2
    public void OnClickDecrease()
    {
        value = int.Parse(txtSelect.text);

        if (value > 2)
        {
            value -= 1;
            txtSelect.text = value.ToString();
        }

        Debug.Log(value);
    }
    //게임 시작
    public void OnClickGameStart()
        {
            SceneManager.LoadScene("LakeScene", LoadSceneMode.Additive);
            TitleUIcanvas.gameObject.SetActive(false);
        GameUIcanvas.gameObject.SetActive(true);
        txtGameStart.gameObject.SetActive(true);
        StartCoroutine(Countdown());

        }

    private IEnumerator Countdown()
    {
        while (countdownTime > 0)
        {
            txtGameStart.text = "TOUCH ALL OF THEM IN " + Mathf.Ceil(countdownTime) + " SECONDS !";
            countdownTime -= Time.deltaTime;
            yield return null;
        }

        txtGameStart.gameObject.SetActive(false);
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

    public void TocuhAgain()
    {
        pnlTouchagain.SetActive(true); // Panel을 활성화

        // 2초 뒤에 비활성화
        StartCoroutine(DeactivatePanelAfterDelay(2f));
    }

    // 일정 시간이 지난 후 Panel을 비활성화하는 코루틴
    private IEnumerator DeactivatePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기

        pnlTouchagain.SetActive(false); // Panel을 비활성화
    }

}



