using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private float duration = 3f;
    [SerializeField] private string emptySceneName = "";
    
    public ScDefine.ScScene CurrentScene { get; private set; }
    public ScDefine.ScScene PreviousScene { get; private set; }

    private string currentSceneName;
    private string prevSceneName;
    private bool isSceneLoading;


    
    private void Awake()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        currentSceneName = activeScene.name;
    }



    public void LoadScene(ScDefine.ScScene scene)
    {
        if(isSceneLoading)
        {
            Debug.LogWarning("씬 로딩 중입니다. 잠시 후 다시 시도해주세요.");
            return;
        }

        isSceneLoading = true;

        PlaySceneBgm(scene);
        RemovePlayerInfoOrNot(scene);

        Load(scene).Forget();
    }

    public void LoadPreviousScene()
    {
        if (!string.IsNullOrEmpty(prevSceneName))
        {
            Load(PreviousScene).Forget();
        }
        else
        {
            Debug.LogWarning("이전 씬 정보가 없습니다.");
        }
    }
    
    public string GetSceneName(ScDefine.ScScene scene)
    {
        return scene switch
        {
            ScDefine.ScScene.Admin                      => "Admin",
            ScDefine.ScScene.TutorialInitial            => "Tut_Init",
            ScDefine.ScScene.TutorialMove               => "Tut_Move",
            ScDefine.ScScene.TutorialCrosswalk          => "Tut_Cross",
            ScDefine.ScScene.TestTutorialInitial        => "InitialTutorial",
            ScDefine.ScScene.TestTutorialMove           => "MoveTutorial",
            ScDefine.ScScene.TestTutorialCrosswalk      => "CrosswalkTutorial",

            ScDefine.ScScene.Ch1Login                   => "Ch1_Login",
            ScDefine.ScScene.Ch1Play                    => "Ch1_Play",
            
            ScDefine.ScScene.Ch2Login                   => "Ch2_Login",
            ScDefine.ScScene.Ch2Play                    => "Ch2_Play",
            ScDefine.ScScene.Ch2BlindSpot               => "Ch2-2_BlindExperience",

            ScDefine.ScScene.Ch3Login                   => "Ch3_Login",
            ScDefine.ScScene.Ch3Room                    => "Ch3_Lobby",
            ScDefine.ScScene.Ch3Play                    => "Ch3_Play",
            
            ScDefine.ScScene.Sg01_SafetyLine            => "Sg01_SafetyLine",
            ScDefine.ScScene.Sg02_LookAround            => "Sg02_LookAround",
            ScDefine.ScScene.Sg03_HandUp                => "Sg03_HandUp",
            ScDefine.ScScene.Sg04_TrafficBlink          => "Sg04_TrafficBlink",
            ScDefine.ScScene.Sg05_SafeWalk              => "Sg05_SafeWalk",
            ScDefine.ScScene.Sg06_BlindSpot             => "Sg06_BlindSpotExperience",
            ScDefine.ScScene.Sg07_GsCarPrediction       => "Sg07_CarPrediction",
            ScDefine.ScScene.Sg08_Jaywalking            => "Sg08_Jaywalking",
            _ => "",
        };
    }

    public void SetCurrentSceneManually(ScDefine.ScScene currentScene)
    {
        prevSceneName = currentSceneName;

        CurrentScene = currentScene;
        currentSceneName = GetSceneName(currentScene);
    }
    
    

    private static void PlaySceneBgm(ScDefine.ScScene scene)
    {
        switch (scene)
        {
            case ScDefine.ScScene.Admin:
            case ScDefine.ScScene.TutorialInitial:
            case ScDefine.ScScene.TutorialMove:
            case ScDefine.ScScene.TutorialCrosswalk:
            case ScDefine.ScScene.TestTutorialInitial:
            case ScDefine.ScScene.TestTutorialMove:
            case ScDefine.ScScene.TestTutorialCrosswalk:
            case ScDefine.ScScene.Ch1Login:
            case ScDefine.ScScene.Ch2Login:
            case ScDefine.ScScene.Ch3Login:
            case ScDefine.ScScene.Ch3Room:
                Manager.Instance.SoundMgr.PlayDefaultBgm();
                break;
            default:
                Manager.Instance.SoundMgr.PlayChapterBgm();
                break;
        }
    }

    private static void RemovePlayerInfoOrNot(ScDefine.ScScene scene)
    {
        switch (scene)
        {
            case ScDefine.ScScene.Admin:
            case ScDefine.ScScene.TutorialInitial:
            case ScDefine.ScScene.Ch1Login:
            case ScDefine.ScScene.Ch2Login:
            case ScDefine.ScScene.Ch3Login:
                Manager.Instance.GameMgr.SetCurrentPlayerInfo(null);
                break;
        }
    }



    private async UniTaskVoid Load(ScDefine.ScScene scene)
    {
        if (CurrentScene != scene)
        {
            string sceneName = GetSceneName(scene);
            
            Debug.Log($"load scene -> {sceneName}");

            await SceneManager.LoadSceneAsync(emptySceneName);
            await UniTask.WaitForSeconds(duration);
            await SceneManager.LoadSceneAsync(sceneName);
            
            PreviousScene = CurrentScene;
            prevSceneName = currentSceneName;
            
            CurrentScene = scene;
            currentSceneName = sceneName;
        }
        
        isSceneLoading = false;
    }
}
