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
    private void OnSpawnNpc(int prefabIndex)
    {
        ScCh3NpcService.Instance.OnSpawnNpc(prefabIndex);
    }
}
