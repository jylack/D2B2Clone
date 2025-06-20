using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public partial class ScCh3PlayService : MonoBehaviourPunCallbacks
{
    public static ScCh3PlayService Instance { get; private set; }
    private const int TOTAL_TIME = 60;

    [SerializeField] private ScTrafficLightSystem trafficLightSystem;
    [SerializeField] private GameObject glowWallParent;
    [SerializeField] private GameObject carSpawnerParent;
    [SerializeField] private GameObject confettiParent;
    [SerializeField] private GameObject carParent;

    public CancellationTokenSource TimeUpCts { get; } = new CancellationTokenSource();

    private Dictionary<int, int> npcIdToActorNumber = new();
    private List<ScCh3ScoreData> scoreDatas = new();
    private double timerStartTime;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Hashtable props = new()
        {
            { ScCh3Define.PROP_KEY_PLAY_LOADED, true },
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }



    public void SetCarParent(ScCar car)
    {
        car.transform.SetParent(carParent.transform);
    }

    public void TryLeaveRoom()
    {
        if (PhotonNetwork.IsMasterClient)
            TransferMasterTo();

        LeaveRoom();

        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch3Login);
    }



    private async UniTaskVoid StartTimer()
    {
        try
        {
            timerStartTime = PhotonNetwork.Time;

            UICh3Play.Instance.UpdateTime(TOTAL_TIME);
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(TimeUpCts.Token, this.GetCancellationTokenOnDestroy());

            while (!linkedCts.IsCancellationRequested)
            {
                await UniTask.Delay(100, cancellationToken: linkedCts.Token);

                double elapsed = PhotonNetwork.Time - timerStartTime;
                int time = Mathf.Clamp((int)(TOTAL_TIME - elapsed), 0, TOTAL_TIME);

                UICh3Play.Instance.UpdateTime(time);

                if (time <= 0)
                {
                    if (PhotonNetwork.IsMasterClient)
                        BroadcastTimeUp();

                    break;
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

    private void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    private void DestoyAllCars()
    {
        int carCount = carParent.transform.childCount;
        if (carCount > 0)
        {
            (0..carCount).ForEach(i =>
            {
                GameObject child = carParent.transform.GetChild(i).gameObject;

                Manager.Instance.ResourceMgr.InstantiateSmokeExplosion(child.transform.position + Vector3.up);
                Destroy(child);
            });
        }
    }
}
