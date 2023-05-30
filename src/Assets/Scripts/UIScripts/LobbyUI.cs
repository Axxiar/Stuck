using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject lobbyOptionsCreate;
    [SerializeField] private GameObject lobbyOptionsJoin;
    [SerializeField] private GameObject lobbyCanvas;


    public void JoinLobbyByCodeCanvas()
    {
        lobbyOptionsCreate.SetActive(false);
        lobbyOptionsJoin.SetActive(true);
        lobbyCanvas.SetActive(false);
        //unlock cursor
        Cursor.lockState = CursorLockMode.None;

    }

    public void lobbyCanvasS()
    {
        lobbyCanvas.SetActive(true);
        lobbyOptionsCreate.SetActive(false);
        lobbyOptionsJoin.SetActive(false);
    }

    public void CreateLobbyCanvas()
    {
        lobbyOptionsCreate.SetActive(false);
        lobbyCanvas.SetActive(true);
        lobbyOptionsJoin.SetActive(false);
        //create lobby
        Lobby.instance.CreateLobby("f");
    }

    public void CreateLobby()
    {
        lobbyOptionsCreate.SetActive(true);
        lobbyCanvas.SetActive(false);
        lobbyOptionsJoin.SetActive(false);
        //unlock cursor
        Cursor.lockState = CursorLockMode.None;
    }

    //change the lobby code text to the lobby code

    void Update()
    {
        
    }


    public void StartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame ()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        if (Lobby.instance.hostLobby != null)
        {
            //destroy lobby
            Lobby.instance.LeaveLobby();
        }
        SceneManager.LoadScene(0);
    }
}
