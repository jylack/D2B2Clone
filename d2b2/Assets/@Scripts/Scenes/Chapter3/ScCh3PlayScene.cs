using Photon.Pun;
using UnityEngine;

public class ScCh3PlayScene : ScSceneBase
{
    [SerializeField] private GameObject[] positionObjects;



    private void Start()
    {
        int posIndex = 0;
        ScDefine.ScGuideCharacter characterType = ScDefine.ScGuideCharacter.Character1;

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(ScCh3Define.PROP_KEY_PLAYER_INDEX, out object posIdx))
            posIndex = (int)posIdx;

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(ScCh3Define.PROP_KEY_CHARACTER_TYPE, out object charType))
            characterType = (ScDefine.ScGuideCharacter)charType;

        Transform playerTransform = positionObjects[posIndex].transform;
        object[] data = { characterType };
        PhotonNetwork.Instantiate("Prefabs/Ch3Player", playerTransform.position, playerTransform.rotation, 0, data);
    }
}
