using System;
using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Setup()
    {
        Debug.Log("PopupManager Setup");
        Instance = new GameObject("[PopupManager]").AddComponent<PopupManager>();
        DontDestroyOnLoad(Instance);
    }

    Dictionary<Type, PopupBase> m_Popups = new Dictionary<Type, PopupBase>();
    Dictionary<Type, PopupLoadOperationBase> m_PopupLoadOperation = new Dictionary<Type, PopupLoadOperationBase>();

    public PopupLoadOperation<T> Load<T>() where T : PopupBase
    {
        if (Instance.m_Popups.TryGetValue(typeof(T), out var existingPopup))
        {
            var result = new PopupLoadOperation<T>();
            result.Done(existingPopup as T);
            return result;
        }
        else if (Instance.m_PopupLoadOperation.TryGetValue(typeof(T), out var loadOperation))
            return loadOperation as PopupLoadOperation<T>;
        else
        {
            var loadOp = new PopupLoadOperation<T>();
            StartCoroutine(CoLoad<T>(loadOp));
            return loadOp;
        }
    }

    IEnumerator CoLoad<T>(PopupLoadOperation<T> request) where T : PopupBase
    {
        var type = typeof(T);
        var requestedPopup = default(PopupBase);
        var attribute = type.GetCustomAttribute<PopupPathAttribute>();
        var req = Resources.LoadAsync<GameObject>(attribute == null ? type.Name : attribute.LoadPath);

        yield return req;
        if (req.asset != null)
        {
            var go = Instantiate(req.asset as GameObject);
            requestedPopup = go == null ? null : go.GetComponent(type) as PopupBase;
        }

        if (requestedPopup == null) Debug.LogException(new System.Exception());

        request.Done(requestedPopup);
    }
}
