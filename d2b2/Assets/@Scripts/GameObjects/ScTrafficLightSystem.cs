using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScTrafficLightSystem : ScObjectBase
{
    private const float intersectionAreaHeight = 5f;

    [SerializeField] private int intervalTime = 1000;
    [SerializeField] private int greenDuration = 6000;
    [SerializeField] private int blinkIntervalTime = 500;
    [SerializeField] private float intersectionAreaSize = 16f;

    [SerializeField]
    [TableList(AlwaysExpanded = true, ShowIndexLabels = true)]
    private List<ScTrafficLightGroup> trafficLightGroups = new List<ScTrafficLightGroup>();

    private int nextTargetIndex;
    private ScTrafficLightGroup currentTrafficLightGroup;
    private Vector3 areaSize => new Vector3(intersectionAreaSize, intersectionAreaHeight, intersectionAreaSize);



    private void Start()
    {
        if (trafficLightGroups?.Count <= 0)
            return;

        foreach (ScTrafficLightGroup group in trafficLightGroups)
            group.SetLight(ScDefine.ScTrafficLightType.Red);

        Run().Forget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }



    private async UniTask Run()
    {
        try
        {
            while (!base.DestroyToken.IsCancellationRequested)
            {
                // 이전 신호등 그룹
                currentTrafficLightGroup?.SetLight(ScDefine.ScTrafficLightType.Red);

                // 변경 간격
                await UniTask.Delay(intervalTime, cancellationToken: base.DestroyToken);

                // 사거리에 차 있는지 체크
                while (true)
                {
                    Collider[] cars = Physics.OverlapBox(transform.position, areaSize / 2, Quaternion.identity, ScDefine.Layer.CarMask);

                    if (cars.Length > 0)
                        await UniTask.Delay(100, cancellationToken: base.DestroyToken);
                    else
                        break;

                    print("car exists.");
                }

                // 다음 신호등 그룹
                currentTrafficLightGroup = trafficLightGroups[nextTargetIndex];
                currentTrafficLightGroup.SetLight(ScDefine.ScTrafficLightType.Green);

                nextTargetIndex = ++nextTargetIndex % trafficLightGroups.Count;

                int waitTime = greenDuration * 3 / 4;
                await UniTask.Delay(waitTime, cancellationToken: base.DestroyToken);

                int elapsedTime = waitTime;

                while (elapsedTime < greenDuration)
                {
                    currentTrafficLightGroup.InvertColor();
                    await UniTask.Delay(blinkIntervalTime, cancellationToken: base.DestroyToken);
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
}
