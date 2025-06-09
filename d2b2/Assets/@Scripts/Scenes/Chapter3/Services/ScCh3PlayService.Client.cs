using Photon.Pun;

public partial class ScCh3PlayService
{
    public void TryCatchNpc(int npcId, int actorNumber)
    {
        photonView.SendToMaster(nameof(OnTryCatchNpc), npcId, actorNumber);
    }



    [PunRPC]
    private void OnAllClientLoaded()
    {

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
    private void OnNpcCatched(int npcId, int actorNumber)
    {

    }
}
