using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private string emptySceneName = "";
    
    private bool isLoaded;
    private string currentSceneName;
    private string prevSceneName;
    
    
    
    private void Awake()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        currentSceneName = activeScene.name;

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }



    public void LoadScene(ScDefine.ScScene scene)
    {
        string sceneName = GetSceneName(scene);
        LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
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
            LoadScene(prevSceneName);
        }
        else
        {
            Debug.LogWarning("이전 씬 정보가 없습니다.");
        }
    }

    private static string GetSceneName(ScDefine.ScScene scene)
    {
        return scene switch
        {
            ScDefine.ScScene.InitSettings               => "InitSettings",
            ScDefine.ScScene.TutorialPlayerSettings     => "Tut_PlayerSettings",
            ScDefine.ScScene.TutorialCharacterSelection => "Tut_CharacterSelection",
            ScDefine.ScScene.TutorialPlay               => "Tut_Play",

            ScDefine.ScScene.Ch1Login                   => "Ch1_Login",
            ScDefine.ScScene.Ch1Play                    => "Ch1_Play",

            ScDefine.ScScene.Ch2Login                   => "Ch2_Login",
            ScDefine.ScScene.Ch2Play                    => "Ch2_Play",
            ScDefine.ScScene.Ch2BlindSpot               => "BlindSpotExperienceGuide",
            ScDefine.ScScene.LookAroundGuide               => "LookAroundGuide",
            ScDefine.ScScene.HandUpGuide             => "HandUpGuide",

            ScDefine.ScScene.Ch3Login                   => "Ch3_Login",
            ScDefine.ScScene.Ch3Room                    => "Ch3_Room",
            ScDefine.ScScene.Ch3Play                    => "Ch3_Play",
            _ => "",
        };
    }
    
    

    private async UniTask Load(string sceneName)
    {
        if (currentSceneName == sceneName)
            return;
        
        Debug.Log($"load scene -> {sceneName}");

        await FadeOut();
        
        // warning error log 방지
        var listener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        if (listener != null)
            Destroy(listener);
        
        await SceneManager.LoadSceneAsync(emptySceneName, LoadSceneMode.Additive);
        
        // unload
        prevSceneName = currentSceneName;
        if (!string.IsNullOrEmpty(currentSceneName))
            await SceneManager.UnloadSceneAsync(currentSceneName);

        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
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
