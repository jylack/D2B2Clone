using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine;

public partial class ScCh3PlayService
{
    public void TryCatchNpc(int npcId, int actorNumber)
    {
        photonView.SendToMaster(nameof(OnTryCatchNpc), npcId, actorNumber);
    }



    [PunRPC]
    private void OnAllClientLoaded()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
            scoreDatas.Add(new ScCh3ScoreData(player.ActorNumber, player.NickName, 0));

        glowWallParent.SetActive(true);
        UICh3Play.Instance.UpdateScores(scoreDatas);
        StartTimer().Forget();
    }

    [PunRPC]
    private void OnUpdateTrafficLights()
    {
        trafficLightSystem.OnUpdateTrafficLight().Forget();
    }

    [PunRPC]
    private void OnSpawnNpc(int npcId, int prefabIndex)
    {
        ScCh3NpcService.Instance.OnSpawnNpc(npcId, prefabIndex);
    }
    
    [PunRPC]
    private void OnUpdateNpcNextAction(int npcId, ScDefine.ScPathPointNextAction nextAction, int newPathPointIndex, bool isRun)
    {
        ScCh3NpcService.Instance.OnUpdateNpcNextAction(npcId, nextAction, newPathPointIndex, isRun);
    }

    [PunRPC]
    private void OnNpcCaught(int npcId, int actorNumber)
    {
        ScCh3NpcService.Instance.OnNpcCaught(npcId, actorNumber);

        ScCh3ScoreData scoreData = scoreDatas.FirstOrDefault(x => x.actorNumber == actorNumber);

        if (scoreData != null)
            scoreData.score++;
        else
            Debug.Log("score data not found.");

        UICh3Play.Instance.UpdateScores(scoreDatas);
    }

    [PunRPC]
    private async void OnTimeUp()
    {
        TimeUpCts.Cancel();

        glowWallParent.SetActive(false);
        trafficLightSystem.SetAllLightColors(ScDefine.ScTrafficLightType.Red);

        DestoyAllCars();
        ScCh3NpcService.Instance.DestroyAllNpcs();

        await UniTask.Delay(1000);

        confettiParent.SetActive(true);
    }
}
