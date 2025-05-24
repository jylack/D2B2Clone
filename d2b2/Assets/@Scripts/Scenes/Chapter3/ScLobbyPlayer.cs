using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class ScLobbyPlayer : ScObjectBase
{
    [SerializeField] private TMP_Text nickNameText;
    [SerializeField] private GameObject[] characterPrefabs;

    public int ActorNumber { get; private set; }
    public int PositionIndex { get; private set; }
    public ScDefine.ScGuideCharacter GuideCharacterType { get; private set; }
    public string NickName { get; private set; }



    private void Start()
    {
        GameObject character = Instantiate(characterPrefabs[(int)GuideCharacterType], transform);
        character.transform.localPosition = Vector3.zero;
        character.transform.localRotation = Quaternion.identity;

        nickNameText.text = NickName;
        
        if (PhotonNetwork.LocalPlayer.ActorNumber == ActorNumber)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            
            Hashtable props = new()
            {
                { ScCh3Define.PROP_KEY_IS_READY, true },
                { ScCh3Define.PROP_KEY_PLAYER_INDEX, PositionIndex }
            };

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        
            Debug.Log($"PositionIndex: {PositionIndex}");
        }
    }



    public void Init(ScLobbyPlayerEntity playerEntity)
    {
        ActorNumber = playerEntity.actorNumber;
        PositionIndex = playerEntity.positionIndex;
        GuideCharacterType = playerEntity.guideCharacterType;
        NickName = playerEntity.nickname;
    }
}
