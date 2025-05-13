using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject nameMenu;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private GameObject charaSelectMenu;
    [SerializeField] private GameObject characters;
    public ScPlayerEntity playerEntity;

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

    public void EndSetting()
    {
        settingMenu.SetActive(false);
        OpenCharaSelect();
    }

    public void OpenCharaSelect()
    {
        charaSelectMenu.SetActive(true);
        characters.SetActive(true);
    }
}
