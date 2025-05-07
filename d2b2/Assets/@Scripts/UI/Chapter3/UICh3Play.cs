using UnityEngine;

public class UICh3Play : UIBase
{
    public void GoToLogin()
    {
        base.LoadScene(Define.ScScene.Ch3Login);
    }
}