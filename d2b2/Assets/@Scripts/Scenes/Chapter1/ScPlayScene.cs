using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScPlayScene : ScSceneBase
{
    [SerializeField] private List<ScDefine.ScScene> MovingSceneList;

    private void Start()
    {
        bool isMovableScene = false;
        string currentSceneName = SceneManager.GetActiveScene().name;

        foreach (var sceneName in MovingSceneList)
        {
            if (currentSceneName == sceneName.ToString())
            {
                isMovableScene = true;
                break;
            }
        }

        if (!isMovableScene)
            Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(0);
    }

}