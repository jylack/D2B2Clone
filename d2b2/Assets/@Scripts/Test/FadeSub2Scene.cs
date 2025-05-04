using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FadeSub2Scene : MonoBehaviour
{
    private AudioListener listener;
    
    
    
    private async UniTaskVoid Awake()
    {
        try
        {
            Debug.Log($"load {nameof(FadeSub2Scene)}");
            
            listener = GameObject.Find("Main Camera").GetComponent<AudioListener>();

            await UniTask.Delay(1000);
        
            Manager.Instance.SceneMgr.OnSceneLoaded();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
    
    
    
    public void LoadScene()
    {
        Manager.Instance.SceneMgr.LoadScene("FadeSub1", () =>
        {
            Destroy(listener);
        });
    }
}