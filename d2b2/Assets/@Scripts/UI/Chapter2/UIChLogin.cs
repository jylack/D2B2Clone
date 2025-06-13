using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIChLogin : UIBase
{
    [Header("UI")]
    [SerializeField] private GameObject signIn;
    [SerializeField] private GameObject signInSuccess;
    [Header("Before Login")]
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private ScDefine.ScScene nextSceneType;
    [Header("After Login")]
    [SerializeField] private TMP_Text signedInNickName;

    private bool isProcessing;



    public async void OnClickLogin()
    {
        try
        {
            Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Ok);

            if (isProcessing)
                return;

            isProcessing = true;
            
            if (!CheckValidation())
                return;

            ScPlayerEntity playerEntity = await Manager.Instance.DbMgr.Load(nameInput.text);
            if (playerEntity == null)
            {
                SetErrorMessage("존재하지 않는 아이디입니다");
                return;
            }

            Manager.Instance.GameMgr.SetCurrentPlayerInfo(playerEntity);
            signedInNickName.text = playerEntity.nickName;

            errorText.gameObject.SetActive(false);
            signIn.SetActive(false);
            signInSuccess.SetActive(true);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        finally
        {
            isProcessing = false;
        }
    }

    public void OnClickStartGame()
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Ok);

        if (isProcessing)
            return;

        isProcessing = true;
        
        base.LoadScene(nextSceneType);
    }

    public void OnClickSettings()
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Ok);

        if (isProcessing)
            return;

        isProcessing = true;

        // Show Settings UI

    }

    public void OnClickExit()
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Cancel);

        if (isProcessing)
            return;

        isProcessing = true;
        
        base.LoadRootScene();
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
