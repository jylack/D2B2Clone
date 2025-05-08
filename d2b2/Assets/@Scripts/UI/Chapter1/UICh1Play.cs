using UnityEngine;

public class UICh1Play : UIBase
{
    public void GoToLogin()
    {
        base.LoadScene(ScDefine.ScScene.Ch1Login);
    }
}