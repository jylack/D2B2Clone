using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ScSceneBase : ScObjectBase
{
    protected virtual async void Awake()
    {
        try
        {
            await UniTask.Delay(1000, cancellationToken: base.DestroyToken);
            Manager.Instance.SceneMgr.OnSceneLoaded();
        }
        catch (OperationCanceledException ex)
        {
            Debug.Log(ex.Message);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}