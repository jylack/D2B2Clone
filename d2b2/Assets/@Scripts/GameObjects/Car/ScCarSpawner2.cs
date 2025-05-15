using Cysharp.Threading.Tasks;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class ScCarSpawner2 : ScObjectBase
{
    [Header("Time(Milliseconds)")]
    [SerializeField] private int startDelay;
    [SerializeField] private int minSpawnIntervalTime = 1000;
    [SerializeField] private int maxSpawnIntervalTime = 3000;
    [Header("Move Speed")]
    [SerializeField] private float minMoveSpeed = 1f;
    [SerializeField] private float maxMoveSpeed = 2f;
    [Header("Etc")]
    [SerializeField] private bool showGizmoLine = true;
    [SerializeField] private float distance = 10f;
    [SerializeField] private Vector3 direction;
    [SerializeField] private GameObject[] carPrefabs;



    private async void Start()
    {
        try
        {
            if (startDelay > 0)
                await UniTask.Delay(startDelay, cancellationToken: DestroyToken);

            RunSpawn().Forget();
        }
        catch (OperationCanceledException ex)
        {
            Debug.Log(ex.Message);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);

        if (showGizmoLine)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + (direction * distance));
        }
    }



    private static int GetRandomValue(int value, int value2)
    {
        return UnityEngine.Random.Range(value, value2);
    }

    private static float GetRandomValue(float value, float value2)
    {
        return UnityEngine.Random.Range(value, value2);
    }



    private async UniTask RunSpawn()
    {
        try
        {
            while (!DestroyToken.IsCancellationRequested)
            {
                int randomInterval = GetRandomValue(minSpawnIntervalTime, maxSpawnIntervalTime);
                await UniTask.Delay(randomInterval, cancellationToken: DestroyToken);

                SpawnRandomCar();
            }
        }
        catch (OperationCanceledException ex)
        {
            Debug.Log(ex.Message);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    private void SpawnRandomCar()
    {
        float moveSpeed = GetRandomValue(minMoveSpeed, maxMoveSpeed);
        int carIndex = GetRandomValue(0, carPrefabs.Length);
        GameObject carPrefab = carPrefabs[carIndex];

        Instantiate(carPrefab, transform)
            .GetComponent<ScCar>()
            .Init(moveSpeed, direction);
    }
}
