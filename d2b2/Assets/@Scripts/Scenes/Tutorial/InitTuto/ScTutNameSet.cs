using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScTutNameSet : MonoBehaviour
{
    [SerializeField] private GameObject inputName;
    [SerializeField] private GameObject showName;
    [SerializeField] private GameObject saveName;

    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text successText;

    [SerializeField] private TMP_Text nameShowText;
    [SerializeField] private TMP_Text savedNameText;

    private void SetErrorMessage(string msg)
    {
        successText.text = "";
        errorText.text += "\n" + msg;
    }

    private void SetSuccessMessage(string msg)
    {
        errorText.text = "";
        successText.text = msg;
    }

    public void CheckNameValid()
    {
        successText.text = "";
        errorText.text = "";
        ScDefine.ScNickNameValidation validation = ScUtils.CheckNickNameValidation(nameInput.text);

        if (validation == ScDefine.ScNickNameValidation.Empty)
        {
            SetErrorMessage("입력칸이 비었습니다");
            return;
        }

        if (validation == ScDefine.ScNickNameValidation.LessThan2Char)
        {
            SetErrorMessage("이름의 길이가 2보다 짧습니다");
            return;
        }

        if (validation == ScDefine.ScNickNameValidation.IncompleteHangul)
        {
            SetErrorMessage("한글로만 입력해주세요");
            return;
        }

        CheckName();
    }

    private async void CheckName()
    {
        bool check = await Manager.Instance.DbMgr.CheckNickNameExist(nameInput.text);
        if (check == true)
        {
            SetErrorMessage("이미 있는 이름입니다.\n다른 이름을 입력해주세요");
            return;
        }

        ShowName(nameInput.text);
    }

    public void SaveName()
    {

        string name = nameInput.text.Trim();
        ScPlayerEntity entity = new ScPlayerEntity
        {
            nickName = name,
            settings = new ScPlayerSettingsEntity()
        };
        TutorialManager.Instance.playerEntity = entity;
        Manager.Instance.GameMgr.SetCurrentPlayerInfo(entity);

        Manager.Instance.DbMgr.Save(name, entity).Forget();

        SetSuccessMessage("생성 성공");

        inputName.SetActive(false);
        showName.SetActive(false);
        saveName.SetActive(true);
        savedNameText.text = name;
    }

    public void ShowInput()
    {
        inputName.SetActive(true);
        showName.SetActive(false);
    }

    public void ShowName(string name)
    {
        inputName.SetActive(false);
        showName.SetActive(true);

        nameShowText.text = name;
    }

    public void ToAdmin()
    {
        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Admin);
    }

    public void ChangeToSetting()
    {
        TutorialManager.Instance.EnableSetting();
    }
}
