using UnityEngine;

public abstract class UIBase : ScObjectBase
{
    protected void LoadScene(ScDefine.ScScene scene)
    {
        Debug.Log("여기 잘오나");
        Manager.Instance.SceneMgr.LoadScene(scene);
    }

    public void LoadRootScene()
    {
        Debug.Log("??");
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.InitSettings);
    }
}