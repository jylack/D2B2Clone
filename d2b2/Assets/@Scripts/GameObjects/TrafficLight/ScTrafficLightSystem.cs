using Cysharp.Threading.Tasks;
using Photon.Pun;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ScTrafficLightSystem : ScObjectBase
{
    [SerializeField] private int timeBeforeColorChange = 2000;
    [SerializeField] private int greenDuration = 6000;
    [SerializeField] private int blinkIntervalTime = 500;
    [SerializeField] private bool usePhoton;

    [SerializeField]
    [TableList(AlwaysExpanded = true, ShowIndexLabels = true)]
    private List<ScTrafficLightGroup> trafficLightGroups = new();

    private int nextTargetIndex;
    private ScTrafficLightGroup currentTrafficLightGroup;
    private CancellationTokenSource greenBeforeEventCts;
    private CancellationTokenSource updateTrafficLightsCts;



    private void Start()
    {
        if (trafficLightGroups?.Count <= 0)
            return;

        foreach (ScTrafficLightGroup group in trafficLightGroups)
            group.SetLight(ScDefine.ScTrafficLightType.Red);

        if (usePhoton)
        {
            if (PhotonNetwork.IsMasterClient)
                RunWithPhotonTrafficSignal().Forget();
        }
        else
        {
            Run().Forget();
        }
    }



    public void OnCrosswalkEnter()
    {
        print("OnCrosswalkEnter");
    }

    public void OnCrosswalkExited()
    {
        print("OnCrosswalkExited");
    }

    public async UniTaskVoid OnUpdateTrafficLight()
    {
        try
        {
            updateTrafficLightsCts?.Cancel();
            updateTrafficLightsCts?.Dispose();
            updateTrafficLightsCts = new CancellationTokenSource();
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(updateTrafficLightsCts.Token, base.DestroyToken);

            WaitThenRaiseGreenBeforeEvent(updateTrafficLightsCts.Token).Forget();
            await UpdateTrafficLights(linkedCts.Token);

            updateTrafficLightsCts.Dispose();
            updateTrafficLightsCts = null;
        }
        catch (InvalidOperationException ex)
        {
            Debug.Log(ex.Message);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }



    private async UniTask Run()
    {
        try
        {
            while (!base.DestroyToken.IsCancellationRequested)
            {
                WaitThenRaiseGreenBeforeEvent(base.DestroyToken).Forget();
                await UpdateTrafficLights(base.DestroyToken);
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

    private async UniTask RunWithPhotonTrafficSignal()
    {
        try
        {
            while (!base.DestroyToken.IsCancellationRequested)
            {
                ScCh3PlayService.Instance.SendUpdateTrafficLightsToMaster();
                await UniTask.Delay(greenDuration);
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

    private async UniTask UpdateTrafficLights(CancellationToken token)
    {
        try
        {
            // 이전 신호등 그룹
            currentTrafficLightGroup?.SetLight(ScDefine.ScTrafficLightType.Red);

            // 다음 신호등 그룹
            currentTrafficLightGroup = trafficLightGroups[nextTargetIndex];
            currentTrafficLightGroup.SetLight(ScDefine.ScTrafficLightType.Green);

            nextTargetIndex = ++nextTargetIndex % trafficLightGroups.Count;

            int blinkBeforeTime = greenDuration * 3 / 4;
            await UniTask.Delay(blinkBeforeTime, cancellationToken: token);

            int elapsedTime = blinkBeforeTime;

            currentTrafficLightGroup.OnStartGreenLightBlink();

            // 녹색불 점멸
            if (usePhoton)
            {
                while (!token.IsCancellationRequested)
                {
                    currentTrafficLightGroup.InvertColor();
                    await UniTask.Delay(blinkIntervalTime, cancellationToken: token);
                }
            }
            else
            {
                
                while (elapsedTime < greenDuration)
                {
                    currentTrafficLightGroup.InvertColor();
                    await UniTask.Delay(blinkIntervalTime, cancellationToken: token);
                    elapsedTime += blinkIntervalTime;
                }
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

    private async UniTask WaitThenRaiseGreenBeforeEvent(CancellationToken token)
    {
        try
        {
            await UniTask.Delay(greenDuration - timeBeforeColorChange, cancellationToken: token);

            foreach (ScTrafficLight trafficLight in trafficLightGroups[nextTargetIndex].items)
                trafficLight.Ready();
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
