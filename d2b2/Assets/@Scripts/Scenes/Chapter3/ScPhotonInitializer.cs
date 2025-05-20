using Photon.Pun;
using UnityEngine;

public class ScPhotonInitializer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        string playerName = Manager.Instance.GameMgr.NicknName;

        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }
}
