using UnityEngine;

public class UIInitSettings : UIBase
{
    public void GoToTutorial()
    {
        base.LoadScene(Define.ScScene.TutorialPlayerSettings);
    }
    
    public void GoToChapter1()
    {
        base.LoadScene(Define.ScScene.Ch1Login);
    }
    
    public void GoToChapter2()
    {
        base.LoadScene(Define.ScScene.Ch2Login);
    }
    
    public void GoToChapter3()
    {
        base.LoadScene(Define.ScScene.Ch3Login);
    }
}
