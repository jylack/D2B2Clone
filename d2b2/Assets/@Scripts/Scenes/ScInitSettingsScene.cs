using UnityEngine;

public class ScInitSettingsScene : ScSceneBase
{
    public static bool firstInit = true;
    protected override void Awake()
    {
        if (firstInit == true)
        {
            Manager.Instance.Init();
            firstInit = false;
        }
        else
        {
            base.Awake();
        }

    }
}
