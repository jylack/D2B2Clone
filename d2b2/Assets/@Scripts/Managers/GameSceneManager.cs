using System;
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
    
    
    
    private void Awake()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        currentSceneName = activeScene.name;

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }



    public void LoadScene(string sceneName)
    {
        Load(sceneName).Forget();
    }

    public void OnSceneLoaded()
    {
        isLoaded = true;
    }



    private async UniTask Load(string sceneName)
    {
        if (currentSceneName == sceneName)
            return;
        
        Debug.Log($"load scene: {sceneName}");

        await FadeOut();
        
        // warning error log 방지
        var listener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        if (listener != null)
            Destroy(listener);
        
        await SceneManager.LoadSceneAsync(emptySceneName, LoadSceneMode.Additive);
        
        // unload
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
        await canvasGroup.DOFade(1f, duration);
    }

    private async UniTask FadeIn()
    {
        await canvasGroup.DOFade(0f, duration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
