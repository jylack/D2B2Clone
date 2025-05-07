using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ScCrossWalk : MonoBehaviour
{
    [Header("Time Set")]
    [SerializeField] float stopTime;
    [SerializeField] float moveTime;

    [SerializeField] Material redLightMat;
    [SerializeField] Material greenLightMat;
    [SerializeField] MeshRenderer trafficLight;
    Collider crossCollider;

    private void Awake()
    {
        crossCollider = GetComponent<Collider>();
        TrafficLightTask().Forget();
    }

    async UniTaskVoid TrafficLightTask()
    {
        while(true)
        {
            crossCollider.enabled = true;
            trafficLight.material = redLightMat;
            await UniTask.Delay(TimeSpan.FromSeconds(stopTime));

            crossCollider.enabled = false;
            trafficLight.material = greenLightMat;
            await UniTask.Delay(TimeSpan.FromSeconds(moveTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Triggered");
        }
    }
}
