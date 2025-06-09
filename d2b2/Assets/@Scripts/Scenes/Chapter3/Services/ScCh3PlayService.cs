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
    private const int TOTAL_TIME = 120;

    [SerializeField] private ScTrafficLightSystem trafficLightSystem;
    //[SerializeField] private ScCarSpawner2[] carSpawners;

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



    private async UniTaskVoid StartTimer()
    {
        try
        {
            timerStartTime = PhotonNetwork.Time;

            UICh3Play.Instance.UpdateTime(TOTAL_TIME);
            CancellationToken token = this.GetCancellationTokenOnDestroy();

            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(100, cancellationToken: token);

                double elapsed = PhotonNetwork.Time - timerStartTime;
                int time = Mathf.Clamp((int)(TOTAL_TIME - elapsed), 0, TOTAL_TIME);

                UICh3Play.Instance.UpdateTime(time);

                if (time <= 0)
                    break;
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
