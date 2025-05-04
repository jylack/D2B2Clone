using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FadeSub1Scene : MonoBehaviour
{
    private AudioListener listener;
    
    
    
    private async UniTaskVoid Awake()
    {
        try
        {
            Debug.Log($"load {nameof(FadeSub1Scene)}");

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
        Manager.Instance.SceneMgr.LoadScene("FadeSub2", () =>
        {
            Destroy(listener);
        });
    }
}