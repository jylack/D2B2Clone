using Cysharp.Threading.Tasks;
using Photon.Pun;
using System.Threading;
using TMPro;
using UnityEngine;

public class UILobby : UIBase
{
    [SerializeField] private TextMeshProUGUI nickNameText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private int maxSeconds = 10;
    [SerializeField] private GameObject playerParent;
    [SerializeField] private GameObject startButton;

    private int seconds;
    private CancellationTokenSource countdownCts;



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
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        string sceneName = Manager.Instance.SceneMgr.GetSceneName(ScDefine.ScScene.Ch3Play);
        PhotonNetwork.LoadLevel(sceneName);
    }
    
    public void OnExitButtonClicked()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (!ScCh3LobbyService.Instance.TransferMasterTo())
                ScCh3LobbyService.Instance.LeaveRoom();
        }
        else
        {
            ScCh3LobbyService.Instance.LeaveRoom();
        }

        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch3Login);
    }

    public void OnMasterChanged(bool isMaster)
    {
        startButton.SetActive(isMaster);
    }



    private void SetCountdown(int count)
    {
        countdownText.text = count.ToString();
    }

    private async UniTask StartCountdown()
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
    }
}
