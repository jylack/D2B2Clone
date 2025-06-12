using Cysharp.Threading.Tasks;
using Photon.Pun;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICh3Lobby : UIBase
{
    [SerializeField] private TextMeshProUGUI nickNameText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private int maxSeconds = 10;
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private int seconds;
    private CancellationTokenSource countdownCts;



    private void Start()
    {
        nickNameText.text = Manager.Instance.GameMgr.NickName;
    }



    public void RestartCountdown()
    {
        countdownCts?.Cancel();
        countdownCts = null;

        seconds = maxSeconds;
        SetCountdown(seconds);
        countdownText.gameObject.SetActive(true);

        StartCountdown().Forget();
    }

    public void StopCountdown()
    {
        countdownText.gameObject.SetActive(false);
        countdownCts?.Cancel();
    }

    public void OnStartButtonClicked()
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Ok);

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        ScCh3LobbyService.Instance.BroadcastStartGame();
    }

    public void OnExitButtonClicked()
    {
        Manager.Instance.SoundMgr.PlaySfx(ScDefine.ScSound.Cancel);

        if (PhotonNetwork.IsMasterClient)
            ScCh3LobbyService.Instance.TransferMasterTo();
        
        ScCh3LobbyService.Instance.LeaveRoom();

        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch3Login);
    }

    public void OnMasterChanged(bool isMaster)
    {
        startButton.gameObject.SetActive(isMaster);
    }

    public void OnStartGame()
    {
        startButton.interactable = false;
        exitButton.interactable = false;

        RestartCountdown();
    }



    private void SetCountdown(int count)
    {
        countdownText.text = count.ToString();
    }

    private async UniTask StartCountdown()
    {
        try
        {
            countdownCts?.Cancel();
            countdownCts?.Dispose();
            countdownCts = new CancellationTokenSource();
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(countdownCts.Token, base.DestroyToken);

            while (seconds > 0)
            {
                await UniTask.WaitForSeconds(1, cancellationToken: linkedCts.Token);

                if (linkedCts.IsCancellationRequested)
                    return;

                SetCountdown(--seconds);
            }

            countdownCts?.Dispose();
            countdownCts = null;

            await UniTask.Delay(1000, cancellationToken: base.DestroyToken);

            string sceneName = Manager.Instance.SceneMgr.GetSceneName(ScDefine.ScScene.Ch3Play);
            PhotonNetwork.LoadLevel(sceneName);
        }
        catch (OperationCanceledException ex)
        {
            Debug.Log(ex.Message);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
