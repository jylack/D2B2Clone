using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScChLogin : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text successText;

    public void ClickLoginBtn()
    {

    }
    public void CheckNameValid()
    {
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
    private void SetErrorMessage(string msg)
    {
        successText.text = "";
        errorText.text = msg;
    }
    private void SetSuccessMessage(string msg)
    {
        errorText.text = "";
        successText.text = msg;
    }
    private async void CheckName()
    {
        bool check = await Manager.Instance.DbMgr.CheckNickNameExist(nameInput.text);
        if (check == true)
        {
            var temp = await Manager.Instance.DbMgr.Load(nameInput.text);
            return;
        }
    }

    public void LoadSecondStage()
    {

    }
    public void LoadFirstStage()
    {


    }
    public void LoadThirdStage()
    {

    }
}
