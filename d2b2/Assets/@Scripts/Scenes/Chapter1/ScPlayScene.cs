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
            //Debug.Log($"ScDefine.ScScene: {sceneName}");

            if (currentSceneName == Manager.Instance.SceneMgr.GetSceneName(sceneName))
            {
                isMovableScene = true;
                break;
            }
        }



        if (!isMovableScene)
        {
            //Debug.LogWarning($"Scene {currentSceneName} 이동불가씬");
            Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(0);
        }
        else
        {
            //Debug.Log($"Scene {currentSceneName} 이동가능씬");
            Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(1);
        }

    }


    private void Start()
    {
        if(SceneManager.sceneCount > 1)
        {
            currentSceneName = SceneManager.GetSceneAt(1).name;            
        }
        else
        {
            currentSceneName = SceneManager.GetActiveScene().name;
        }
        //Debug.Log($"현재 씬: {currentSceneName}");
           
        Init().Forget();
    }

}