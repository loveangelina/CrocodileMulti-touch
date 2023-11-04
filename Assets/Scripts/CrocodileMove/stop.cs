using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop : MonoBehaviour
{
    public bool isStop;
    // Start is called before the first frame update
    public void Stop()
    {
        transform.position += new Vector3(1, 0, 0);
    }
}
