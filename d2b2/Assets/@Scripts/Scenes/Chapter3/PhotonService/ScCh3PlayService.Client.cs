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
}
