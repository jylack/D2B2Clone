using System;
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
                try
                {
                    var managerPrefab = Resources.Load<GameObject>("Prefabs/Manager");
                    instance = Instantiate(managerPrefab).GetComponent<Manager>();
                    DontDestroyOnLoad(instance.gameObject);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }

            return instance;
        }
    }
    
    [SerializeField] private GameObject gameSceneManagerPrefab;
    [SerializeField] private GameObject soundManagerPrefab;
    [SerializeField] private GameObject resourceManagerPrefab;
    
    public InputManager InputMgr { get; private set; }
    public GameManager GameMgr { get; private set; }
    public GameSceneManager SceneMgr { get; private set; }
    public ResourceManager ResourceMgr { get; private set; }
    public DatabaseManager DbMgr { get; private set; }
    public SoundManager SoundMgr { get; private set; }
    public LanguageManager LanguageMgr { get; private set; }




    private async void Awake()
    {
        try
        {
            InputMgr = InitSubManager<InputManager>();
            GameMgr = InitSubManager<GameManager>();
        
            DbMgr = InitSubManager<DatabaseManager>();
            await DbMgr.Init();
            
            ResourceMgr = Instantiate(resourceManagerPrefab).GetComponent<ResourceManager>();
            ResourceMgr.transform.SetParent(transform);
            
            SceneMgr = Instantiate(gameSceneManagerPrefab).GetComponent<GameSceneManager>();
            SceneMgr.transform.SetParent(transform);
            
            SoundMgr = Instantiate(soundManagerPrefab).GetComponent<SoundManager>();
            SoundMgr.transform.SetParent(transform);

            LanguageMgr = InitSubManager<LanguageManager>();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }



    public void Init()
    {
        Debug.Log("manager initialized.");
    }



    private TComp InitSubManager<TComp>() where TComp : Component
    {
        TComp comp = new GameObject(typeof(TComp).Name).AddComponent<TComp>();
        comp.transform.SetParent(transform);

        return comp;
    }
}
