using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public partial class ScCh3PlayService
{
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
    
    
    
    public void SendUpdateTrafficLightsToAll()
    {
        photonView.Broadcast(nameof(OnUpdateTrafficLights));
    }
}
