public class ScAdminScene : ScSceneBase
{
    public static ScAdminScene Instance { get; private set; }

    private static bool isFirstInit;



    protected override void Awake()
    {
        Instance = this;

        if (!isFirstInit)
        {
            Manager.Instance.Init();
            isFirstInit = true;
            base.IsLoaded = true;
        }
        else
        {
            base.Awake();
        }
    }
}
