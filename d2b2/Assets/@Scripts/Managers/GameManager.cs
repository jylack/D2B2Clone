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
    public string NickName { get; private set; }

    public void SetPlayer(ScPlayer player)
    {
        Player = player;
    }

    public void SetNickName(string nickname)
    {
        NickName = nickname;
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