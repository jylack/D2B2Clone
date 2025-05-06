using UnityEngine;

public abstract class UIBase : ScObjectBase
{
    protected void LoadScene(Define.ScScene scene)
    {
        Manager.Instance.SceneMgr.LoadScene(scene);
    }

    public void LoadRootScene()
    {
        Manager.Instance.SceneMgr.LoadScene(Define.ScScene.InitSettings);
    }
}