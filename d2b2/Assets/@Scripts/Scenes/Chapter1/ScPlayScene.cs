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
            Debug.Log($"Checking ScDefine.ScScene: {sceneName}");                        

            if (currentSceneName == Manager.Instance.SceneMgr.GetSceneName(sceneName))
            {
                //Debug.Log($"Scene: {currentSceneName} 이동가능씬");
                isMovableScene = true;
                break;
            }
        }

        if (!isMovableScene)
        {
            Debug.LogWarning($"Scene {currentSceneName} 이동불가씬");
            Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(0);
        }
        else
        {
            Debug.Log($"Scene {currentSceneName} 이동가능씬");
            Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(1);
        }

    }

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        Debug.Log($"Current Scene: {currentSceneName}");


        Init().Forget();
    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    currentSceneName = scene.name;
    //    Debug.Log($"Scene Loaded: {currentSceneName}");
    //    Init().Forget();
    //}

}