using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class ScPlayerPhotonService : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] characterPrefabs;
    
    private Dictionary<int, GameObject> playerListEntries;



    private void Start()
    {
        // string playerName = Manager.Instance.GameMgr.NickName;
        string playerName = "Test";

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
        RoomOptions options = new RoomOptions { MaxPlayers = ScCh3Define.MaxPlayerCount };
        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public override void OnJoinedRoom()
    {
        WriteDebugLog("Join Success!");

        if (playerListEntries == null)
            playerListEntries = new Dictionary<int, GameObject>();

        int posX = 0;
        
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.CustomProperties.TryGetValue(ScCh3Define.PhotonCustomPropKeys.CHARACTER_TYPE, out object characterTypeObj))
            {
                int idx = (int)characterTypeObj % characterPrefabs.Length;
                GameObject prefab = characterPrefabs[idx];
                GameObject character = Instantiate(prefab);
                
                Vector3 pos = character.transform.position;
                pos.x = posX;
                character.transform.position = pos;

                posX += 2;
            }
        }

        Hashtable props = new()
        {
            { ScCh3Define.PhotonCustomPropKeys.PLAYER_LOADED_LEVEL, false }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }


    
    private void StartGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        PhotonNetwork.LoadLevel("DemoAsteroids-GameScene");
    }

    private void WriteDebugLog(string msg)
    {
        Debug.Log($"[Photon] {msg}");
    }
}
