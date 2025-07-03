using Cysharp.Threading.Tasks;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScCh3NpcService : ScObjectBase
{
    public static ScCh3NpcService Instance { get; private set; }

    [SerializeField] private Transform spawnPointParent;
    [SerializeField] private Transform movePointParent;
    [SerializeField] private GameObject[] npcPrefabs;

    private readonly List<ScPathPoint> spawnPoints = new();
    private readonly List<ScPathPoint> movePoints = new();
    private readonly List<ScPathPoint> totalPoints = new();

    private Dictionary<int, ScCh3Npc> npcDict = new();
    private int remainNpcCount = 16;
    private int currentNpcCount;
    private int npcIncreasedId;
    private int badThingTokenCount;     // NPC가 나쁜행동을 할 수 있는 개수
    private int badThingTokenGenInterval = 5000;



    private void Awake()
    {
        badThingTokenGenInterval -= (PhotonNetwork.PlayerList.Length * 500);
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
        RunGenerateBadThingToken().Forget();
    }



    public void BroadcastUpdateNpcNextAction(int npcId, ScDefine.ScPathPointNextAction nextAction, ScPathPoint pathPoint = null, bool isRun = false)
    {
        int newPathPointIndex = 0;

        if (pathPoint != null)
            newPathPointIndex = totalPoints.IndexOf(pathPoint);

        ScCh3PlayService.Instance.BroadcastUpdateNpcNextAction(npcId, nextAction, newPathPointIndex, isRun);
    }

    public bool DecreaseBadThingToken()
    {
        if (badThingTokenCount > 0)
        {
            badThingTokenCount--;
            return true;
        }

        return false;
    }

    public void DestroyAllNpcs()
    {
        foreach (ScCh3Npc npc in npcDict.Values)
            npc.DestroyNpc();

        npcDict.Clear();
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

    public void OnUpdateNpcNextAction(int npcId, ScDefine.ScPathPointNextAction nextAction, int newPathPointIndex, bool isRun)
    {
        if (npcDict.TryGetValue(npcId, out ScCh3Npc npc))
        {
            ScPathPoint newPathPoint = totalPoints[newPathPointIndex];
            npc.UpdateNextAction(nextAction, newPathPoint, isRun);
        }
        else
        {
            Debug.Log($"npc not found. npcId[{npcId}]");
        }
    }

    public void OnNpcDestroy(int npcId)
    {
        npcDict.Remove(npcId);
        currentNpcCount--;
    }

    public void OnNpcCaught(int npcId, int actorNumber)
    {
        if (npcDict.TryGetValue(npcId, out ScCh3Npc npc))
            npc.OnNpcCaught(npcId, actorNumber);
        else
            Debug.Log("npc not found.");
    }

    

    private async UniTaskVoid RunSpawn()
    {
        try
        {
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ScCh3PlayService.Instance.TimeUpCts.Token, base.DestroyToken);

            while (!linkedCts.IsCancellationRequested)
            {
                await UniTask.Delay(1000, cancellationToken: linkedCts.Token);

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

    private async UniTaskVoid RunGenerateBadThingToken()
    {
        try
        {
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ScCh3PlayService.Instance.TimeUpCts.Token, base.DestroyToken);

            while (!linkedCts.IsCancellationRequested)
            {
                await UniTask.Delay(badThingTokenGenInterval, cancellationToken: linkedCts.Token);
                badThingTokenCount++;
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
