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
            if(Manager.Instance.GameMgr.Player != null)
            {
                Manager.Instance.GameMgr.ApplyCurrentSetting();
            }
            else
            {
                ScPlayerSettingsEntity entity = new();
                entity.renderScale = 1f;
                entity.brightness = 2f;

                entity.colorWeakMode = 0;
                entity.colorWeakCompensate = 100f;

                entity.rotateMode = 2;
                entity.rotationAngle = 3f;
                entity.rotationSpeed = 6f;

                entity.masterVolume = 1f;
                entity.bgmVolume = 1f;
                entity.voiceVolume = 1f;
                entity.soundEffectVolume = 1f;

                Manager.Instance.GameMgr.ApplyLoadedSetting(entity);
            }
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