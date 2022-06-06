using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersionNumber : MonoBehaviour
{
 
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI mTMP = gameObject.GetComponent<TextMeshProUGUI>();
        mTMP.text = "Version " + Application.version.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
