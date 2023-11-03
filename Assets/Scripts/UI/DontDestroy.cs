using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        // 현재 게임 오브젝트를 파괴하지 않도록 설정
        DontDestroyOnLoad(this.gameObject);
    }
}
