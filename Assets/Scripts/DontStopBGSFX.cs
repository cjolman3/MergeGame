using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontStopBGSFX : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
