using UnityEngine;

public class UITutCharacterSelection : UIBase
{
    public void GoToPlay()
    {
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.TutorialMove);
    }
}