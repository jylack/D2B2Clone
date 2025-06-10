using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private string emptySceneName = "";
    
    public ScDefine.ScScene CurrentScene { get; private set; }
    public ScDefine.ScScene PreviousScene { get; private set; }

    private bool isLoaded;
    private string currentSceneName;
    private string prevSceneName;
    
    
    
    private void Awake()
    {
        bg.enabled = true;
        Scene activeScene = SceneManager.GetActiveScene();
        currentSceneName = activeScene.name;

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    
    
    public void LoadScene(ScDefine.ScScene scene)
    {
        PreviousScene = CurrentScene;
        CurrentScene = scene;

        string sceneName = GetSceneName(scene);
        Load(sceneName).Forget();
    }

    public void OnSceneLoaded()
    {
        isLoaded = true;
    }

    public void LoadPreviousScene()
    {
        if (!string.IsNullOrEmpty(prevSceneName))
        {
            Load(prevSceneName).Forget();
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
            ScDefine.ScScene.InitSettings               => "InitSettings",
            ScDefine.ScScene.TutorialInitial            => "Tut_Init",
            ScDefine.ScScene.TutorialMove               => "Tut_Move",
            ScDefine.ScScene.TutorialCrosswalk          => "Tut_Cross",
            ScDefine.ScScene.TestTutorialInitial        => "InitialTutorial",
            ScDefine.ScScene.TestTutorialMove           => "MoveTutorial",
            ScDefine.ScScene.TestTutorialCrosswalk      => "CrosswalkTutorial",

            ScDefine.ScScene.Ch1Login                   => "Ch1_Login",
            ScDefine.ScScene.Ch1Play                    => "Ch1_Play",
            
            ScDefine.ScScene.Ch2Login                   => "Ch2_Login",
            ScDefine.ScScene.Ch2Play                    => "SecondStage",

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



    private async UniTask Load(string sceneName)
    {
        if (currentSceneName == sceneName)
            return;
        
        Debug.Log($"load scene -> {sceneName}"); 

        await FadeOut();

        // warning error log 방지
        GameObject camera = GameObject.Find("Main Camera");
        if (camera != null)
        {
            var listener = camera.GetComponent<AudioListener>();
            if (listener != null)
                Destroy(listener);
        }

        await SceneManager.LoadSceneAsync(emptySceneName, LoadSceneMode.Additive);

        // unload
        prevSceneName = currentSceneName;
        if (!string.IsNullOrEmpty(currentSceneName))
            await SceneManager.UnloadSceneAsync(currentSceneName);

        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // 활성 씬을 새로 로드된 씬으로 설정합니다.
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        currentSceneName = sceneName;

        isLoaded = false;
        await UniTask.WaitUntil(() => isLoaded);

        await SceneManager.UnloadSceneAsync(emptySceneName);

        await FadeIn();
    }

    private async UniTask FadeOut()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        await canvasGroup.DOFade(1f, duration).ToUniTask();
    }

    private async UniTask FadeIn()
    {
        await canvasGroup.DOFade(0f, duration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
