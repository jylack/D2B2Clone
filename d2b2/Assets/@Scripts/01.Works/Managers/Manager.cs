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
    public ResourceManager ResourceMgr { get; private set; }



    private void Init()
    {
        InputMgr = new GameObject(nameof(InputManager)).AddComponent<InputManager>();
        InputMgr.transform.SetParent(Instance.transform);

        ResourceMgr = new GameObject(nameof(ResourceManager)).AddComponent<ResourceManager>();
        ResourceMgr.transform.SetParent(Instance.transform);
    }
}
