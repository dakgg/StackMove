using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBase : MonoBehaviour
{

}


public abstract class PopupLoadOperationBase : CustomYieldInstruction
{
    public bool IsDone { get; private set; }
    public bool IsCancelled { get; private set; }

    protected PopupBase m_Loaded;
    public event EventHandler OnDone;
    public override bool keepWaiting => !IsDone;

    internal void Cnacel()
    {
        if (IsDone) return;
        IsCancelled = true;
        IsDone = true;
        OnDone?.Invoke(this, null);
    }

    internal void Done(PopupBase popupBase)
    {
        if (IsCancelled) return;
        m_Loaded = popupBase;
        IsDone = true;
        // if (m_Loaded != null) m_Loaded.SetupViewEventCaller();
        OnDone?.Invoke(this, null);
    }
}

public class PopupLoadOperation<T> : PopupLoadOperationBase where T : PopupBase
{
    public T Result => m_Loaded as T;
    public T GetResult() => m_Loaded as T;
}


[AttributeUsage(AttributeTargets.Class)]
public class PopupPathAttribute : System.Attribute
{
    public string LoadPath { get; private set; }

    public PopupPathAttribute(string path) => LoadPath = path;
}