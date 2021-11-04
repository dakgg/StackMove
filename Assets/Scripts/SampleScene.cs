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

        var a = LayerMask.NameToLayer("Actor");
        Debug.Log($"{1<<a}");
        var b = LayerMask.NameToLayer("Test1");
        Debug.Log($"{1<<b}");
        var c = LayerMask.NameToLayer("Test2");
        Debug.Log($"{1<<c}");
    }
}
