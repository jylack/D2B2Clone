using ExitGames.Client.Photon;
using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public partial class ScLobbyService
{
    public bool TransferMasterTo()
    {
        if (!PhotonNetwork.IsMasterClient)
            return false;
        
        int actorIndex = GetFirstActorIndexExceptMe();
        if (actorIndex < 0)
            return false;

        Player player = PhotonNetwork.CurrentRoom.GetPlayer(actorNumbersForPosition[actorIndex]);
        PhotonNetwork.SetMasterClient(player);

        LeaveRoom();
        return true;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        int posIndex = GetPositionIndex(otherPlayer.ActorNumber);
        actorNumbersForPosition[posIndex] = 0;

        ScLeftPlayerEntity leftPlayerEntity = new(otherPlayer.ActorNumber, actorNumbersForPosition);
        string json = JsonConvert.SerializeObject(leftPlayerEntity);
        
        Broadcast(nameof(OnPlayerLeft), json);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (!changedProps.TryGetValue(ScCh3Define.PROP_KEY_IS_READY, out object isReady))
            return;

        if (!(bool)isReady)
        {
            Broadcast(nameof(OnCountdownStateChanged), false);
            return;
        }
        
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (!CheckIsReady(player.CustomProperties))
            {
                Broadcast(nameof(OnCountdownStateChanged), false);
                return;
            }
        }

        Broadcast(nameof(OnCountdownStateChanged), true);
    }

    
    
    private static bool CheckIsReady(Hashtable props)
    {
        if (props.TryGetValue(ScCh3Define.PROP_KEY_IS_READY, out object isReady))
        {
            if (!(bool)isReady)
            {
                // ScCh3Define.PROP_KEY_IS_READY == false
                return false;
            }
        }
        else
        {
            // ScCh3Define.PROP_KEY_IS_READY 키 없음
            return false;
        }

        return true;
    }
    


    [PunRPC]
    private void OnJoinedRoom_Server(string json)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        ScLobbyPlayerEntity playerEntity = JsonConvert.DeserializeObject<ScLobbyPlayerEntity>(json);

        // response: 방금 접속한 유저 -> 모든 유저 정보 전달
        Player sender = PhotonNetwork.CurrentRoom.GetPlayer(playerEntity.actorNumber);

        List<ScLobbyPlayerEntity> playerEntities = GetAllPlayerEntities();
        string respJson = JsonConvert.SerializeObject(playerEntities);

        Photon.RPC(nameof(OnJoinedRoom_ServerResponse), sender, respJson);

        // send all: 모든 유저 -> 방금 접속한 유저 정보 전달
        int emptyPosIdx = GetEmptyPositionIndex();
        playerEntity.positionIndex = emptyPosIdx;
        actorNumbersForPosition[emptyPosIdx] = playerEntity.actorNumber;

        string sendJson = JsonConvert.SerializeObject(playerEntity);
        Broadcast(nameof(OnAddNewPlayer), sendJson);
    }
    
    private void Broadcast(string methodName, params object[] parameters)
    {
        Photon.RPC(methodName, RpcTarget.AllViaServer, parameters);
    }
}
