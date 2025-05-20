using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class ScPhotonInitializer : MonoBehaviourPunCallbacks
{
    private const int maxPlayers = 6;

    private Dictionary<int, GameObject> playerListEntries;



    private void Start()
    {
        string playerName = Manager.Instance.GameMgr.NicknName;

        if (!string.IsNullOrEmpty(playerName))
        {
            WriteDebugLog("try connecting...");

            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }



    public override void OnConnectedToMaster()
    {
        WriteDebugLog("JoinRandomRoom...");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        WriteDebugLog("CreateRoom...");

        string roomName = "Room " + Random.Range(1000, 10000);
        RoomOptions options = new RoomOptions { MaxPlayers = maxPlayers };
        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public override void OnJoinedRoom()
    {
        WriteDebugLog("Join Success!");

        if (playerListEntries == null)
            playerListEntries = new Dictionary<int, GameObject>();

        //foreach (Player p in PhotonNetwork.PlayerList)
        //{
        //    if (p.CustomProperties.TryGetValue(AsteroidsGame.PLAYER_READY, out object isPlayerReady))
        //    {
        //        entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool)isPlayerReady);
        //    }

        //    playerListEntries.Add(p.ActorNumber, entry);
        //}

        Hashtable props = new()
        {
            { AsteroidsGame.PLAYER_LOADED_LEVEL, false }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }



    private void WriteDebugLog(string msg)
    {
        Debug.Log($"[Photon] {msg}");
    }
}
