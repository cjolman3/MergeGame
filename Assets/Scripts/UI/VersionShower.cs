using UnityEngine;
using TMPro;

public class VersionShower : MonoBehaviour
{
    private void Awake()
    {
        if(TryGetComponent(out TextMeshProUGUI mTMP))
            mTMP.text = Application.version;
    }
}