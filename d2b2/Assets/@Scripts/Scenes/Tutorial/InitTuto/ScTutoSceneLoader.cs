using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTutoSceneLoader : MonoBehaviour
{
    [SerializeField] private ScDefine.ScScene sceneName;

    public void LoadSelectedScene()
    {
        Manager.Instance.SceneMgr.LoadScene(sceneName);
    }
}
