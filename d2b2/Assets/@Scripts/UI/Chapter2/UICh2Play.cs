using UnityEngine;

public class UICh2Play : UIBase
{
    public void GoToLogin()
    {
        base.LoadScene(Define.ScScene.Ch2Login);
    }
}