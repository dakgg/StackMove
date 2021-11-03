using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
    void Start()
    {
        PopupManager.Instance.Load<APopup>();
    }
}
