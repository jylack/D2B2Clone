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
            ScLobbyService.Instance.TransferMasterTo();
        }
        else
        {
            ScLobbyService.Instance.LeaveRoom();
        }

        //foreach (Transform child in playerParent.transform)
        //    Destroy(child.gameObject);

        Manager.Instance.SceneMgr.LoadScene(ScDefine.ScScene.Ch3Login);
    }



    private void SetCountdown(int count)
    {
        countdownText.text = count.ToString();
    }

    private async UniTask StartCountdown()
    {
        countdownCts = new CancellationTokenSource();
        var linkedcts = CancellationTokenSource.CreateLinkedTokenSource(countdownCts.Token, base.DestroyToken);

        while (seconds > 0)
        {
            await UniTask.Delay(1000, cancellationToken: linkedcts.Token);

            if (countdownCts.IsCancellationRequested)
                return;

            SetCountdown(--seconds);
        }
    }
}
