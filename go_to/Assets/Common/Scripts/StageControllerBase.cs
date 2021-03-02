using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class StageControllerBase : MonoBehaviour
{
    protected virtual void Start()
    {
#if UNITY_EDITOR
        if (GameRootManager.Instance == null)
            Initialize();
#endif
    }
    public abstract void Initialize();
}