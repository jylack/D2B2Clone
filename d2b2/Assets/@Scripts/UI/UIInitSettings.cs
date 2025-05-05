using UnityEngine;

public class UIInitSettings : ScObjectBase
{
    public void GoToTutorial()
    {
        Manager.Instance.SceneMgr.LoadScene("Tut_PlayerSettings");
    }
    
    public void GoToChapter1()
    {
        Manager.Instance.SceneMgr.LoadScene("Ch1_Login");
    }
    
    public void GoToChapter2()
    {
        Manager.Instance.SceneMgr.LoadScene("Ch2_Login");
    }
    
    public void GoToChapter3()
    {
        Manager.Instance.SceneMgr.LoadScene("Ch3_Login");
    }
}
