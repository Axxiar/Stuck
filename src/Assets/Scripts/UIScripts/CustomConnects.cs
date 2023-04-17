using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomConnects : MonoBehaviour
{



    //Connect to ip address in the input field named "AddresseIp" in the canvas
    public void ConnectToIp()
    {
        NetworkManager.singleton.networkAddress = GameObject.Find("AddresseIP").GetComponent<TMPro.TMP_InputField>().text;
        Debug.Log("Connecting to " + NetworkManager.singleton.networkAddress);
        NetworkManager.singleton.StartClient();
    }
}
