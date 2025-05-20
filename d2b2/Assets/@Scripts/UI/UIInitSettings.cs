using UnityEngine;

public class UIInitSettings : UIBase
{
    public void GoToTutorial()
    {
        base.LoadScene(ScDefine.ScScene.TutorialInitial);
    }
    
    public void GoToChapter1()
    {
        base.LoadScene(ScDefine.ScScene.Ch1Login);
    }
    
    public void GoToChapter2()
    {
        base.LoadScene(ScDefine.ScScene.Ch2Login);
    }
    
    public void GoToChapter3()
    {
        base.LoadScene(ScDefine.ScScene.Ch3Login);
    }
}
