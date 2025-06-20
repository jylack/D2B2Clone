using ExitGames.Client.Photon;
using Photon.Pun;

public static class ScCh3Assistant
{
    public static bool ComparePropertyValue(Hashtable props, string key, bool validValue)
    {
        if (props.TryGetValue(key, out object value))
        {
            if ((bool)value != validValue)
                return false;
        }
        else
        {
            // 키 없음
            return false;
        }

        return true;
    }

    // extensions
    public static void Broadcast(this PhotonView photonView, string methodName, params object[] parameters)
    {
        photonView.RPC(methodName, RpcTarget.AllViaServer, parameters);
    }

    public static void SendToMaster(this PhotonView photonView, string methodName, params object[] parameters)
    {
        photonView.RPC(methodName, RpcTarget.MasterClient, parameters);
    }
}