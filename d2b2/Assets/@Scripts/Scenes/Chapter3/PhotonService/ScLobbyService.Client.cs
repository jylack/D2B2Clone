using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public partial class ScLobbyService
{
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Room " + UnityEngine.Random.Range(1000, 10000);
        RoomOptions options = new RoomOptions { MaxPlayers = MaxPlayerCount };
        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public override void OnJoinedRoom()
    {
        ScDefine.ScGuideCharacter characterType = Manager.Instance.GameMgr.GuideCharacterType;

        ScLobbyPlayerEntity playerEntity = new(PhotonNetwork.LocalPlayer.NickName, PhotonNetwork.LocalPlayer.ActorNumber, characterType, -1);
        string sendJson = JsonConvert.SerializeObject(playerEntity);
        photonView.SendToMaster(nameof(OnJoinedRoom_Master), sendJson);

        masterChanged?.Invoke(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        print(newMasterClient);
    }



    [PunRPC]
    private void OnAddNewPlayer(string json)
    {
        ScLobbyPlayerEntity playerEntity = JsonConvert.DeserializeObject<ScLobbyPlayerEntity>(json);

        if (!isVrPlayerInit && PhotonNetwork.LocalPlayer.ActorNumber == playerEntity.actorNumber)
        {
            isVrPlayerInit = true;
            
            Vector3 pos = CalcPosition(playerEntity.positionIndex, 6f);
            pos.y = 1f;
            vrPlayer.position = pos;
            vrPlayer.LookAt(Vector3.zero);

            Vector3 targetPos = 2 * uiLobby.transform.position - vrPlayer.position;
            targetPos.y = uiLobby.transform.position.y;
            uiLobby.transform.LookAt(targetPos);
        }

        AddNewPlayer(playerEntity);
    }

    [PunRPC]
    private void OnCountdownStateChanged(bool isStart)
    {
        if (isStart)
            uiLobby.RestartCountdown();
        else
            uiLobby.StopCountdown();
    }

    [PunRPC]
    private void OnPlayerLeft(string json)
    {
        ScLeftPlayerEntity leftPlayerEntity = JsonConvert.DeserializeObject<ScLeftPlayerEntity>(json);
        actorNumbersForPosition = leftPlayerEntity.actorNumbersForPosition;
        
        if (playerDict.Remove(leftPlayerEntity.actorNumber, out ScLobbyPlayer player))
        {
            Destroy(player.gameObject);
            uiLobby.RestartCountdown();
        }
    }

    [PunRPC]
    private void OnJoinedRoom_ServerResponse(string json)
    {
        List<ScLobbyPlayerEntity> playerEntities = JsonConvert.DeserializeObject<List<ScLobbyPlayerEntity>>(json);
        foreach (ScLobbyPlayerEntity playerEntity in playerEntities)
        {
            AddNewPlayer(playerEntity);
        }
    }
}
