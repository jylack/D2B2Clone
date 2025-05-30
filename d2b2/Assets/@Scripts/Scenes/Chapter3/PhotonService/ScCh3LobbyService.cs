using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class ScCh3LobbyService : MonoBehaviourPunCallbacks
{
    public static ScCh3LobbyService Instance { get; private set; }

    private const int MaxPlayerCount = 6;

    [SerializeField] private Transform vrPlayer;
    [SerializeField] private UILobby uiLobby;
    [SerializeField] private Transform playerParent;
    [SerializeField] private GameObject lobbyPlayerPrefab;
    [SerializeField] private UnityEvent<bool> masterChanged;

    private Dictionary<int, ScLobbyPlayer> playerDict = new();
    private int[] actorNumbersForPosition = new int[6];
    private bool isVrPlayerInit;



    private void Awake()
    {
        Instance = this;

        Manager.Instance.Init();
    }

    private void Start()
    {
        TryConnect(Manager.Instance.GameMgr.NickName);
    }

    
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }



    private static Vector3 CalcPosition(int index, float distance = 5f)
    {
        const float angle = 360f / MaxPlayerCount;
        return Quaternion.Euler(0f, angle * index, 0f) * Vector3.right * distance;
    }


    
    private void TryConnect(string nickName)
    {
        if (!string.IsNullOrEmpty(nickName))
        {
            PhotonNetwork.LocalPlayer.NickName = nickName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }
    
    private int GetFirstActorIndexExceptMe()
    {
        for (int i = 0; i < actorNumbersForPosition.Length; i++)
        {
            int actorNum = actorNumbersForPosition[i];
            if (actorNum != 0 && actorNum != PhotonNetwork.LocalPlayer.ActorNumber)
                return i;
        }

        return -1;
    }
    
    private void AddNewPlayer(ScLobbyPlayerEntity playerEntity)
    {
        ScLobbyPlayer player = Instantiate(lobbyPlayerPrefab, playerParent).GetComponent<ScLobbyPlayer>();

        player.transform.position = CalcPosition(playerEntity.positionIndex);
        player.transform.LookAt(Vector3.zero);
        player.Init(playerEntity);

        playerDict.Add(playerEntity.actorNumber, player);
        actorNumbersForPosition[playerEntity.positionIndex] = playerEntity.actorNumber;
    }

    private List<ScLobbyPlayerEntity> GetAllPlayerEntities()
    {
        List<ScLobbyPlayerEntity> entities = new();

        foreach (ScLobbyPlayer player in playerDict.Values)
            entities.Add(new ScLobbyPlayerEntity(player.NickName, player.ActorNumber, player.GuideCharacterType, player.PositionIndex));

        return entities;
    }
    
    private int GetEmptyPositionIndex()
    {
        for (int i = 0; i < actorNumbersForPosition.Length; i++)
        {
            if (actorNumbersForPosition[i] == 0)
                return i;
        }

        return -1;
    }

    private int GetPositionIndex(int actorNumber)
    {
        for (int i = 0; i < actorNumbersForPosition.Length; i++)
        {
            if (actorNumbersForPosition[i] == actorNumber)
                return i;
        }

        return -1;
    }
}
