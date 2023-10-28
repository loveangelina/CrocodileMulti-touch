using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource; // BGM을 위한 AudioSource 컴포넌트
    public AudioSource sfxSource; // Sound FX를 위한 AudioSource 컴포넌트

    public AudioClip gameStartClip; // 게임 시작 버튼을 누를 때 재생될 효과음
    public AudioClip buttonClickClip; // 나머지 버튼을 누를 때 재생될 효과음

    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<SoundManager>();
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

    // BGM 볼륨 설정
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    // Sound FX 볼륨 설정
    public void SetSoundFxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    // BGM 음소거 설정
    public void MuteBGM(bool mute)
    {
        bgmSource.mute = mute;
    }

    // Sound FX 음소거 설정
    public void MuteSoundFx(bool mute)
    {
        sfxSource.mute = mute;
    }

    public void PlayGameStartSound()
    {
        sfxSource.PlayOneShot(gameStartClip);
    }

    public void PlayButtonClickSound()
    {
        sfxSource.PlayOneShot(buttonClickClip);
    }

}
