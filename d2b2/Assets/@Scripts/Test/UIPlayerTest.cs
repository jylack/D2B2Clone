using TMPro;
using UnityEngine;

public class UIPlayerTest : MonoBehaviour
{
    [SerializeField] private TMP_Text lookingText;
    [SerializeField] private TMP_Text leftUpText;
    [SerializeField] private TMP_Text movingText;
    [SerializeField] private TMP_Text rightUpText;

    

    private void Awake()
    {
        Manager.Instance.GameMgr.OnPlayerHeadTurn += OnPlayerOnPlayerHeadTurn;
        Manager.Instance.GameMgr.OnPlayerHandsUp += OnPlayerHandsUp;
        Manager.Instance.GameMgr.OnPlayerMoving += OnPlayerMoving;
    }
    
    private void OnDestroy()
    {
        Manager.Instance.GameMgr.OnPlayerHeadTurn -= OnPlayerOnPlayerHeadTurn;
        Manager.Instance.GameMgr.OnPlayerHandsUp -= OnPlayerHandsUp;
        Manager.Instance.GameMgr.OnPlayerMoving -= OnPlayerMoving;
    }

    private void OnPlayerOnPlayerHeadTurn(ScDefine.ScHeadTurn headTurn)
    {
        lookingText.text = headTurn.ToString();
    }
    
    private void OnPlayerHandsUp(bool leftHandUp, bool rightHandUp,float distance)
    {
        leftUpText.color = leftHandUp ? Color.blue : Color.gray;
        rightUpText.color = rightHandUp ? Color.blue : Color.gray;
    }
    
    private void OnPlayerMoving(bool isMoving)
    {
        movingText.color = isMoving ? Color.blue : Color.gray;
    }
}