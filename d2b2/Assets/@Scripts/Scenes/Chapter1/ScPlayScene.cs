using FIMSpace.FTools;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScPlayScene : ScSceneBase
{
    [SerializeField] private List<ScDefine.ScScene> MovingSceneList;

    private void Start()
    {
        foreach (var sceneName in MovingSceneList)
        {
            if (SceneManager.GetActiveScene().name != sceneName.ToString())
            {
                Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(0);
            }
        }

    }
}