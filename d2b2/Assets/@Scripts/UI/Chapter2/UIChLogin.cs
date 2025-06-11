using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIChLogin : UIBase
{
    [Header("UI")]
    [SerializeField] private GameObject signIn;
    [SerializeField] private GameObject signInSuccess;
    [Header("Etc")]
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private ScDefine.ScScene nextSceneType;

    private bool isLoggingIn;
    
    
    
    public async void OnClickLoginButton()
    {
        try
        {
            if (isLoggingIn)
                return;

            isLoggingIn = true;

            if (!CheckValidation())
                return;

            ScPlayerEntity playerEntity = await Manager.Instance.DbMgr.Load(nameInput.text);
            if (playerEntity == null)
            {
                SetErrorMessage("존재하지 않는 아이디입니다");
                return;
            }

            Manager.Instance.GameMgr.SetCurrentPlayerInfo(playerEntity);

            if (nextSceneType == ScDefine.ScScene.Ch1Play)
                ScChapter1.CurrentSetp = 0;

            base.LoadScene(nextSceneType);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        finally
        {
            isLoggingIn = false;
        }
    }



    private bool CheckValidation()
    {
        ScDefine.ScNickNameValidation validation = ScUtils.CheckNickNameValidation(nameInput.text);
        switch (validation)
        {
            case ScDefine.ScNickNameValidation.Empty:
                SetErrorMessage("입력칸이 비었습니다");
                return false;
            case ScDefine.ScNickNameValidation.LessThan2Char:
                SetErrorMessage("이름의 길이가 2보다 짧습니다");
                return false;
            case ScDefine.ScNickNameValidation.IncompleteHangul:
                SetErrorMessage("한글로만 입력해주세요");
                return false;
        }

        return true;
    }
    
    private void SetErrorMessage(string msg)
    {
        errorText.text = msg;
    }
}