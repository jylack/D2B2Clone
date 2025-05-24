using Photon.Pun;
using UnityEngine;

public class ScCh3PlayScene : ScSceneBase
{
    [SerializeField] private GameObject playerParent;
    [SerializeField] private GameObject[] positionObjects;

    

    private void Start()
    {
        int posIndex = 0;

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(ScCh3Define.PROP_KEY_PLAYER_INDEX, out object posIdx))
            posIndex = (int)posIdx;

        Transform playerTransform = positionObjects[posIndex].transform;
        GameObject player = PhotonNetwork.Instantiate("Prefabs/Ch3Player", playerTransform.position, playerTransform.rotation);
        player.transform.SetParent(playerParent.transform);
    }
}
