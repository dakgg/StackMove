using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CoLoad());

        IEnumerator CoLoad()
        {
            var op = PopupManager.Instance.Load<APopup>();
            yield return op;
        }
    }
}
