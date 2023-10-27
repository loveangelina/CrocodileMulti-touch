using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance => instance;

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
}
