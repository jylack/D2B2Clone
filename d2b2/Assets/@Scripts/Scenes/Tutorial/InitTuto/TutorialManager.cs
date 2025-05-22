using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject nameMenu;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private GameObject noticeMenu;
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

    public void RemoveTutorialManager()
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
        noticeMenu.SetActive(true);
    }

    public void OpenCharaSelect()
    {
        noticeMenu.SetActive(false);
        charaSelectMenu.SetActive(true);
        characters.SetActive(true);
    }
}
