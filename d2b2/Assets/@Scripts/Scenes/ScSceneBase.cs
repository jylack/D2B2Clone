using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class ScSceneBase : ScObjectBase
{
    public bool IsLoaded { get; protected set; }



    protected virtual async void Awake()
    {
        try
        {
            await UniTask.Delay(1000, cancellationToken: base.DestroyToken);
            Manager.Instance.SceneMgr.OnSceneLoaded();
            IsLoaded = true;

            Manager.Instance.GameMgr.ApplyCurrentSetting();
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



    public void SetIsLoadedFalse()
    {
        IsLoaded = false;
    }
}