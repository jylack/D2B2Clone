using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject nameMenu;
    [SerializeField] private GameObject settingMenu;

    public static TutorialManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void EnableSetting()
    {
        nameMenu.SetActive(false);
        settingMenu.SetActive(true);
    }
}
