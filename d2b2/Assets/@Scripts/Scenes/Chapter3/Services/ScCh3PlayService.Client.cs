using Photon.Pun;

public partial class ScCh3PlayService
{
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
    private void OnSpawnCar(int spawnerIndex, int carIndex, float moveSpeed)
    {
        ScCarSpawner2 spawner = carSpawners[spawnerIndex];
        spawner.SpawnCar(carIndex, moveSpeed);
    }

    [PunRPC]
    private void OnSpawnNpc(int npcId, int prefabIndex)
    {
        ScCh3NpcService.Instance.OnSpawnNpc(npcId, prefabIndex);
    }
    
    [PunRPC]
    private void OnUpdateNpcPathPoint(int npcId)
    {
        ScCh3NpcService.Instance.OnUpdatePathPoint(npcId);
    }
}
