using UnityEngine;

public delegate void OnPlayerHeadTurnHandler(ScDefine.ScHeadTurn headTurn);
public delegate void OnPlayerHandsUpHandler(bool leftHandUp, bool rightHandUp, float distance);
public delegate void OnPlayerMovingHandler(bool isMoving);

public class GameManager : MonoBehaviour
{
    public event OnPlayerHeadTurnHandler OnPlayerHeadTurn;
    public event OnPlayerHandsUpHandler OnPlayerHandsUp;
    public event OnPlayerMovingHandler OnPlayerMoving;

    public ScPlayer Player { get; private set; }
    public string NickName => playerEntity.nickName;
    public ScDefine.ScGuideCharacter GuideCharacterType => playerEntity.guideCharacter;

    private ScPlayerEntity playerEntity;



    public void SetPlayer(ScPlayer player)
    {
        Player = player;
    }

    public void SetCurrentPlayerInfo(ScPlayerEntity playerInfo)
    {
        playerEntity = playerInfo;
    }

    public void RaisePlayerHeadTurnEvent(ScDefine.ScHeadTurn headTurn)
    {
        OnPlayerHeadTurn?.Invoke(headTurn);
    }

    public void RaisePlayerHandsUpEvent(bool isLeftHandUp, bool isRightHandUp,float distance)
    {
        OnPlayerHandsUp?.Invoke(isLeftHandUp, isRightHandUp, distance);
    }

    public void RaisePlayerMovingEvent(bool isMoving)
    {
        OnPlayerMoving?.Invoke(isMoving);
    }
}