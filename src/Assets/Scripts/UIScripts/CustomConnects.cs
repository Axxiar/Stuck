using UnityEngine;
using Mirror;


public class CustomConnects : MonoBehaviour
{

    public static void CreateHost()
    {
        NetworkManager.singleton.StartHost();
    }

    //Connect to ip address in the input field named "AddresseIp" in the canvas
    public void ConnectToIp()
    {
        
        NetworkManager.singleton.networkAddress = "127.0.0.1";
        Debug.Log("Connecting to " + NetworkManager.singleton.networkAddress);
        NetworkManager.singleton.StartClient();
    }
}
