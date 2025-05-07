using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ScSceneBase : ScObjectBase
{
    protected virtual async void Awake()
    {
        try
        {
            await UniTask.Delay(1000);
            Manager.Instance.SceneMgr.OnSceneLoaded();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
}