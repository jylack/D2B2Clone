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
    private const float TOTAL_TIME = 120f;

    [SerializeField] private ScTrafficLightSystem trafficLightSystem;
    //[SerializeField] private ScCarSpawner2[] carSpawners;

    private Dictionary<int, int> npcIdToActorNumber = new();
    private List<ScCh3ScoreData> scoreDatas = new();
    private CancellationTokenSource timerCts;
    private float timerStartTime;



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



    private async UniTaskVoid StartTimer(float startTime)
    {
        try
        {
            timerStartTime = startTime;

            UICh3Play.Instance.UpdateTime((int)TOTAL_TIME);

            timerCts = new CancellationTokenSource();
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(timerCts.Token, this.GetCancellationTokenOnDestroy());

            while (!linkedCts.IsCancellationRequested)
            {
                await UniTask.Delay(100);

                float interval = Time.time - timerStartTime;
                int time = (int)(TOTAL_TIME - interval);

                UICh3Play.Instance.UpdateTime(time);

                if (time <= 0)
                    break;
            }

            // 게임 종료
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
