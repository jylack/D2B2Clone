using UnityEngine;

public class UICh3Play : UIBase
{
    public void GoToLogin()
    {
        base.LoadScene(ScDefine.ScScene.Ch3Login);
    }
}