using UnityEngine;

public class UICh3Room : UIBase
{
    public void GoToPlay()
    {
        base.LoadScene(ScDefine.ScScene.Ch3Play);
    }

    public void GoToBack()
    {
        base.LoadScene(ScDefine.ScScene.Ch3Login);
    }
}