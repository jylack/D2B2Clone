using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject(nameof(Manager)).AddComponent<Manager>();
                instance.Init();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
    
    public InputManager InputMgr { get; private set; }
    public GameManager GameMgr { get; private set; }
    public ResourceManager ResourceMgr { get; private set; }
    public DatabaseManager DbMgr { get; private set; }



    private void Init()
    {
        InputMgr = InitSubManager<InputManager>();
        GameMgr = InitSubManager<GameManager>();
        ResourceMgr = InitSubManager<ResourceManager>();
        
        DbMgr = InitSubManager<DatabaseManager>();
        DbMgr.Init();
    }

    private TComp InitSubManager<TComp>() where TComp : Component
    {
        TComp comp = new GameObject(typeof(TComp).Name).AddComponent<TComp>();
        comp.transform.SetParent(instance.transform);

        return comp;
    }
}
