using Cysharp.Threading.Tasks;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScCh3NpcService : ScObjectBase
{
    public static ScCh3NpcService Instance { get; private set; }

    [SerializeField] private Transform spawnPointParent;
    [SerializeField] private Transform movePointParent;
    [SerializeField] private GameObject[] npcPrefabs;

    private List<ScPathPoint> spawnPoints = new();
    private List<ScPathPoint> movePoints = new();
    private int remainNpcCount = 16;
    private int currentNpcCount;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        (..spawnPointParent.childCount).ForEach(i =>
        {
            spawnPoints.Add(spawnPointParent.GetChild(i).GetComponent<ScPathPoint>());
        });

        (..movePointParent.childCount).ForEach(i =>
        {
            movePoints.Add(movePointParent.GetChild(i).GetComponent<ScPathPoint>());
        });

        RunSpawn().Forget();
    }



    public void OnSpawnNpc(int prefabIndex)
    {

    }



    private async UniTaskVoid RunSpawn()
    {
        try
        {
            //await UniTask.Delay(3000);

            //var spawnPt = spawnPoints[0];
            //var prefab = Resources.Load<GameObject>("Prefabs/Ch3Npc");
            //var npc = Instantiate(prefab).GetComponent<ScCh3Npc>();
            //npc.transform.position = spawnPt.transform.position;
            //npc.SetDestination(movePoints[0]);

            var token = base.DestroyToken;

            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(1000);

                
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
