using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Setup()
    {
        Debug.Log("PopupManager Setup");
        var manager = new GameObject("_PopupManager").AddComponent<PopupManager>();
        DontDestroyOnLoad(manager);
    }

    Dictionary<Type, PopupBase> m_Popups = new Dictionary<Type, PopupBase>();
    Dictionary<Type, PopupLoadOperationBase> m_PopupLoadOperation = new Dictionary<Type, PopupLoadOperationBase>();

    public void Load<T>()
    {
        if(Instance.m_Popups.TryGetValue(typeof(T), out var popup))
        {
            // already exist
        }
        else if(Instance.m_PopupLoadOperation.TryGetValue(typeof(T), out var loadOperation))
        {
            // loading ...
        }
        else
        {
            var loadOp = new PopupLoadOPeration<T>();
            StartCoroutine(CoLoadInternal(loadOp));
        }
    }

    IEnumerator CoLoadInternal<T>(PopupLoadOPeration<T> loadOp)
    {
        m_PopupLoadOperation.Add(typeof(T), loadOp);
        var req = Resources.LoadAsync<GameObject>("");
        yield return req;
        if(req.asset != null)
        {
            var go = Instantiate(req.asset as GameObject);
        }
    }
}
