using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public partial class ScCh3PlayService
{
    public void TransferMasterTo()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (PhotonNetwork.PlayerList.Length <= 1)
            return;

        Player player = PhotonNetwork.PlayerList.FirstOrDefault(x => x != PhotonNetwork.LocalPlayer);
        PhotonNetwork.SetMasterClient(player);
    }

    public void BroadcastUpdateTrafficLights()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        photonView.Broadcast(nameof(OnUpdateTrafficLights));
    }

    public void BroadcastSpawnNpc(int npcId, int prefabIndex)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        photonView.Broadcast(nameof(OnSpawnNpc), npcId, prefabIndex);
    }

    public void BroadcastUpdateNpcNextAction(int npcId, ScDefine.ScPathPointNextAction nextAction, int newPathPointIndex, bool isRun)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        photonView.Broadcast(nameof(OnUpdateNpcNextAction), npcId, nextAction, newPathPointIndex, isRun);
    }

    public void BroadcastTimeUp()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        photonView?.Broadcast(nameof(OnTimeUp));
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (!ScCh3Assistant.ComparePropertyValue(player.CustomProperties, ScCh3Define.PROP_KEY_PLAY_LOADED, true))
                return;
        }

        photonView.Broadcast(nameof(OnAllClientLoaded));
    }



    [PunRPC]
    private void OnTryCatchNpc(int npcId, int actorNumber)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (npcIdToActorNumber.ContainsKey(npcId))
            return;

        npcIdToActorNumber.Add(npcId, actorNumber);

        photonView.Broadcast(nameof(OnNpcCaught), npcId, actorNumber);
    }
}
