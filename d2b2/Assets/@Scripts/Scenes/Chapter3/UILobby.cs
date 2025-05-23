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

    public void OnExitButtonClicked()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (!ScLobbyService.Instance.TransferMasterTo())
                ScLobbyService.Instance.LeaveRoom();
        }
        else
        {
            ScLobbyService.Instance.LeaveRoom();
        }

        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch3Login);
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
        var linkedcts = CancellationTokenSource.CreateLinkedTokenSource(countdownCts.Token, base.DestroyToken);

        while (seconds > 0)
        {
            await UniTask.WaitForSeconds(1, cancellationToken: linkedcts.Token);

            if (linkedcts.IsCancellationRequested)
                return;

            SetCountdown(--seconds);
        }

        countdownCts?.Dispose();
        countdownCts = null;
    }
}
