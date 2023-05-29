using UnityEngine;
using Mirror;


public class CustomConnects : MonoBehaviour
{

    void Start()
    {
        //Stop hosting if we are hosting
        if (NetworkManager.singleton.isNetworkActive)
        {
            NetworkManager.singleton.StopHost();
        }
    }

    public void CreateHost()
    {
        NetworkManager.singleton.StartHost();
    }

    //Connect to ip address in the input field named "AddresseIp" in the canvas
    public void ConnectToIp()
    {
        
        NetworkManager.singleton.networkAddress = GameObject.Find("AddresseIP").GetComponent<TMPro.TMP_InputField>().text;
        Debug.Log("Connecting to " + NetworkManager.singleton.networkAddress);
        NetworkManager.singleton.StartClient();
    }

    //stop hosting
    public void StopHost()
    {
        NetworkManager.singleton.StopHost();
    }
}
