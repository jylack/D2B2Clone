using Cysharp.Threading.Tasks;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScCh3NpcService : ScObjectBase
{
    public static ScCh3NpcService Instance { get; private set; }

    [SerializeField] private Transform spawnPointParent;
    [SerializeField] private Transform movePointParent;
    [SerializeField] private GameObject[] npcPrefabs;

    private List<ScPathPoint> spawnPoints = new();
    private List<ScPathPoint> movePoints = new();
    private List<ScPathPoint> totalPoints = new();
    private int remainNpcCount = 16;
    private int currentNpcCount;
    private int npcIncreasedId;
    private Dictionary<int, ScCh3Npc> npcDict = new();



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        (..spawnPointParent.childCount).ForEach(i =>
        {
            spawnPoints.Add(spawnPointParent.GetChild(i).GetComponent<ScPathPoint>());
        });

        (..movePointParent.childCount).ForEach(i =>
        {
            movePoints.Add(movePointParent.GetChild(i).GetComponent<ScPathPoint>());
        });

        totalPoints.AddRange(spawnPoints);
        totalPoints.AddRange(movePoints);
        
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        RunSpawn().Forget();
    }



    public void OnSpawnNpc(int npcId, int prefabIndex)
    {
        int spawnPointIndex = npcId % spawnPoints.Count;
        ScPathPoint spawnPoint = spawnPoints[spawnPointIndex];
        GameObject prefab = npcPrefabs[prefabIndex];

        var npc = Instantiate(prefab).GetComponent<ScCh3Npc>();
        npc.Init(npcId, spawnPoint);
        
        npcDict.Add(npcId, npc);
    }

    public void OnUpdatePathPoint(int npcId)
    {
        if (npcDict.TryGetValue(npcId, out ScCh3Npc npc))
            npc.UpdateNextAction();
    }

    public void OnNpcDestroy(int npcId)
    {
        npcDict.Remove(npcId);
        currentNpcCount--;
    }



    private async UniTaskVoid RunSpawn()
    {
        try
        {
            var token = base.DestroyToken;

            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(1000, cancellationToken: token);

                if (currentNpcCount < remainNpcCount)
                {
                    currentNpcCount++;
                    int prefabIndex = Random.Range(0, npcPrefabs.Length);
                    ScCh3PlayService.Instance.BroadcastSpawnNpc(npcIncreasedId++, prefabIndex);
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
