using UnityEngine;

public class UICh1Play : UIBase
{
    public void GoToLogin()
    {
        base.LoadScene(Define.ScScene.Ch1Login);
    }
}