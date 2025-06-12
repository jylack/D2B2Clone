public class ScAdminScene : ScSceneBase
{
    public static ScAdminScene Instance { get; private set; }



    protected override void Awake()
    {
        Instance = this;

        if (!Manager.IsInit)
        {
            Manager.Instance.Init();
            base.IsLoaded = true;
        }
        else
        {
            base.Awake();
        }
    }
}
