using ExitGames.Client.Photon;
using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public partial class ScCh3LobbyService
{
    public void TransferMasterTo()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        int actorIndex = GetFirstActorIndexExceptMe();
        if (actorIndex < 0)
            return;

        Player player = PhotonNetwork.CurrentRoom.GetPlayer(actorNumbersForPosition[actorIndex]);
        PhotonNetwork.SetMasterClient(player);
    }

    public void BroadcastStartGame()
    {
        photonView.Broadcast(nameof(OnStartGame));
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        int posIndex = GetPositionIndex(otherPlayer.ActorNumber);
        actorNumbersForPosition[posIndex] = 0;

        ScLeftPlayerEntity leftPlayerEntity = new(otherPlayer.ActorNumber, actorNumbersForPosition);
        string json = JsonConvert.SerializeObject(leftPlayerEntity);

        photonView.Broadcast(nameof(OnPlayerLeft), json);
    }

    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    //{
    //    if (!PhotonNetwork.IsMasterClient)
    //        return;
        
    //    foreach (Player player in PhotonNetwork.PlayerList)
    //    {
    //        if (!ScCh3Assistant.ComparePropertyValue(player.CustomProperties, ScCh3Define.PROP_KEY_IS_READY, true))
    //        {
    //            photonView.Broadcast(nameof(OnCountdownStateChanged), false);
    //            return;
    //        }
    //    }

    //    photonView.Broadcast(nameof(OnCountdownStateChanged), true);
    //}



    [PunRPC]
    private void OnJoinedRoom_Master(string json)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        ScLobbyPlayerEntity playerEntity = JsonConvert.DeserializeObject<ScLobbyPlayerEntity>(json);

        // response: 방금 접속한 유저 -> 모든 유저 정보 전달
        Player sender = PhotonNetwork.CurrentRoom.GetPlayer(playerEntity.actorNumber);

        List<ScLobbyPlayerEntity> playerEntities = GetAllPlayerEntities();
        string respJson = JsonConvert.SerializeObject(playerEntities);

        photonView.RPC(nameof(OnJoinedRoom_ServerResponse), sender, respJson);

        // send all: 모든 유저 -> 방금 접속한 유저 정보 전달
        int emptyPosIdx = GetEmptyPositionIndex();
        playerEntity.positionIndex = emptyPosIdx;
        actorNumbersForPosition[emptyPosIdx] = playerEntity.actorNumber;

        string sendJson = JsonConvert.SerializeObject(playerEntity);
        photonView.Broadcast(nameof(OnAddNewPlayer), sendJson);
    }
}
