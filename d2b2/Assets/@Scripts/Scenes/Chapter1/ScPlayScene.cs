using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScPlayScene : ScSceneBase
{
    //이동가능씬 리스트
    [SerializeField] private List<ScDefine.ScScene> MovingSceneList;

    private bool isMovableScene = false;
    private string currentSceneName;

    private async UniTaskVoid Init()
    {
        await UniTask.WaitUntil(() => Manager.Instance.SceneMgr != null);

        
        foreach (var sceneName in MovingSceneList)
        {
            if (currentSceneName == Manager.Instance.SceneMgr.GetSceneName(sceneName))
            {
                isMovableScene = true;
                break;
            }
        }


        //이동가능씬인지 판별후 현재 플레이어의 이동속도를 설정해서 이동가능 불가능 씬을 설정해줍니다.
        if (isMovableScene == false) 
        {
            Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(0);
        }
        else
        {
            Manager.Instance.GameMgr.SetCurrentPlayerMoveSpeed(1);
        }

    }


    private void Start()
    {
        //임시씬(FadeScene)이 아닌 사용중인 씬의 이름을 가져옵니다.
        if (SceneManager.sceneCount > 1)
        {
            currentSceneName = SceneManager.GetSceneAt(1).name;            
        }
        else
        {
            currentSceneName = SceneManager.GetActiveScene().name;
        }
           
        Init().Forget();
    }

}