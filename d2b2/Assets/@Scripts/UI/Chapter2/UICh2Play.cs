using UnityEngine;

public class UICh2Play : UIBase
{
    public void GoToLogin()
    {
        base.LoadScene(ScDefine.ScScene.Ch2Login);
    }
}