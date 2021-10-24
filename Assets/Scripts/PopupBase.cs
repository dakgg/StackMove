using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBase : MonoBehaviour
{

}


public abstract class PopupLoadOperationBase : CustomYieldInstruction
{
    public bool IsDone { get; private set; }
    protected PopupBase m_Loaded;
    public override bool keepWaiting => !IsDone;
}

public class PopupLoadOPeration<T> : PopupLoadOperationBase
{

}