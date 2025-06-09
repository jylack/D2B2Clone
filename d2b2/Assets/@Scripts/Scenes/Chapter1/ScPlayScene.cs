using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScPlayScene : ScSceneBase
{
    [SerializeField] private List<ScDefine.ScScene> MovingSceneList;

    private bool isMovableScene = false;
    private string currentSceneName;

    private async UniTaskVoid Init()
    {
        await UniTask.WaitUntil(() => Manager.Instance.SceneMgr != null);

        foreach (var sceneName in MovingSceneList)
        {
            //Debug.Log($"Checking Scene: {sceneName}");                        

            if (currentSceneName == Manager.Instance.SceneMgr.GetSceneName(sceneName))
            {
                //Debug.Log($"Scene {sceneName} 이동가능씬");
                isMovableScene = true;
                break;
            }
        }

        if (!isMovableScene)
        {
            //Debug.LogWarning($"Scene {currentSceneName} 이동불가씬");
            Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(0);
        }

    }

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        //Debug.Log($"Current Scene: {currentSceneName}");


        Init().Forget();
    }


}

