using System;
using TMPro;
using UnityEngine;

public class UIInitSettings : UIBase
{
    private const string initLoginId = "admin";

    [SerializeField] private GameObject inputPanel;
    [SerializeField] private GameObject chapterButtonPanel;
    [SerializeField] private TMP_InputField inputId;
    [SerializeField] private TMP_InputField inputPassword;



    public void OnOkClicked()
    {
        if (inputId.text.Equals(initLoginId, StringComparison.OrdinalIgnoreCase) 
         && inputPassword.text.Equals(initLoginId, StringComparison.OrdinalIgnoreCase))
        {
            inputPanel.SetActive(false);
            chapterButtonPanel.SetActive(true);
        }
    }

    public void GoToTutorial()
    {
        base.LoadScene(ScDefine.ScScene.TutorialInitial);
    }
    
    public void GoToChapter1()
    {
        base.LoadScene(ScDefine.ScScene.Ch1Login);
    }
    
    public void GoToChapter2()
    {
        base.LoadScene(ScDefine.ScScene.Ch2Login);
    }
    
    public void GoToChapter3()
    {
        base.LoadScene(ScDefine.ScScene.Ch3Login);
    }
}
