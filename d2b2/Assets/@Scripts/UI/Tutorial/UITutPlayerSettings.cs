using UnityEngine;

public class UITutPlayerSettings : UIBase
{
    public void GoToCharacterSelection()
    {
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.TutorialCharacterSelection);
    }
}