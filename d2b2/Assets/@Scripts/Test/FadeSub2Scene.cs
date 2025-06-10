using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FadeSub2Scene : MonoBehaviour
{
    private async void Awake()
    {
        try
        {
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
        //Manager.Instance.SceneMgr.LoadScene("FadeSub1");
    }
}