using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class UserAccountManager : MonoBehaviour
{
    public static UserAccountManager instance;

    public static string LoggedInUsername;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public bool LogIn()
    {
        //get the username from the input field named "Nom" in the canvas
        LoggedInUsername = GameObject.Find("Nom").GetComponent<TMPro.TMP_InputField>().text; ;
        if (LoggedInUsername.Length < 1)
        {
            //call animation named "Shake" on the element named "Nom" in the canvas
            GameObject.Find("Nom").GetComponent<Animator>().Play("Shake");
            return false;
        }
        Debug.Log("Logged in as " + LoggedInUsername);
        return true;
    }

    public void CreateHost()
    {
        if (!LogIn())
        {
            return;
        }
        NetworkManager.singleton.StartHost();
    }

    //Connect to ip address in the input field named "AddresseIp" in the canvas
    public void ConnectToIp()
    {
        if (!LogIn())
        {
            return;
        }
        string address = GameObject.Find("AddresseIP").GetComponent<TMPro.TMP_InputField>().text;
        Debug.Log(address);
        if (address.Length < 1)
        {
            //call animation named "Shake" on the element named "AddresseIP" in the canvas
            GameObject.Find("AddresseIP").GetComponent<Animator>().Play("Shake2");
            return;
        }
        NetworkManager.singleton.networkAddress = address;
        Debug.Log("Connecting to " + NetworkManager.singleton.networkAddress);
        NetworkManager.singleton.StartClient();
    }
}
