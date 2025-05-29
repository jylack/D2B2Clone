using Photon.Pun;

public partial class ScCh3PlayService
{
    public void SendUpdateTrafficLightsToMaster()
    {
        photonView.SendToMaster(nameof(OnUpdateTrafficLights_Master));
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
}
