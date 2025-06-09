using FIMSpace.FTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScPlayScene : ScSceneBase
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().name != ScDefine.ScScene.Ch1Play.ToString())
        {
            Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(0);
        }
    }
}