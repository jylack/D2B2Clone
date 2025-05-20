using TMPro;
using UnityEngine;

public class UIChLogin : UIBase
{
    //public void GoToPlay()
    //{
    //    base.LoadScene(ScDefine.ScScene.Ch2Play);
    //}
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text successText;
    [SerializeField] private ScDefine.ScScene nextSceneType;

    public void OnClickLoginButton()
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
        LoadNicknameData();
    }
    private void SetErrorMessage(string msg)
    {
        successText.text = "";
        errorText.text = "faild : " +  msg;
    }
    private void SetSuccessMessage(string msg)
    {
        errorText.text = "";
        successText.text = "success : " +  msg  + " 님";
    }
    private async void LoadNicknameData()
    {
        var temp = await Manager.Instance.DbMgr.Load(nameInput.text);
        if (temp == null)
        {
            SetErrorMessage("존재하지 않는 아이디입니다");
            return;
        }
        else
        {
            Manager.Instance.GameMgr.SetNickname(temp.nickName);
            SetSuccessMessage(temp.nickName);

            if (nextSceneType == ScDefine.ScScene.Ch1Play)
            {
                Ch1_Step.CurrentSetp = 0;
            }

            base.LoadScene(nextSceneType);
        }
    }
}