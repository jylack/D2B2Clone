using UnityEngine;

public class UITutPlay : UIBase
{
    public void GoToPlayerSettings()
    {
        Manager.Instance.SceneMgr.LoadScene(Define.ScScene.TutorialPlayerSettings);
    }
}