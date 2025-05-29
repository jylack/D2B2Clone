using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;

public partial class ScCh3PlayService : MonoBehaviourPunCallbacks
{
    public static ScCh3PlayService Instance { get; private set; }

    [SerializeField] private ScTrafficLightSystem trafficLightSystem;



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
}
