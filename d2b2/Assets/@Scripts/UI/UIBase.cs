using UnityEngine;

public abstract class UIBase : ScObjectBase
{
    protected void LoadScene(ScDefine.ScScene scene)
    {
        Manager.Instance.SceneMgr.LoadScene(scene);
    }

    public void LoadRootScene()
    {
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.InitSettings);
    }
}