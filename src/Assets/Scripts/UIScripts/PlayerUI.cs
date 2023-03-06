using System;
using UnityEngine;
using Mirror;

public class PlayerUI : NetworkBehaviour
{
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject hudUI;

    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        //on cache le menu pause
        pauseMenuUI.SetActive(false);
        //on affiche le HUD
        hudUI.SetActive(true);
        //on defreeze le jeu
        Time.timeScale = 1f;
        //on bloque le curseur
        Cursor.lockState = CursorLockMode.Locked;

        GameIsPaused = false;
    }
    public void PauseGame()
    {
        //on freeze le jeu
        Time.timeScale = 0f;
        //on cache le HUD
        hudUI.SetActive(false);
        //on affiche le menu pause
        pauseMenuUI.SetActive(true);
        //on débloque le curseur
        Cursor.lockState = CursorLockMode.Confined;

        GameIsPaused = true;
    }
    public void QuitGame()
    {
        // ssi le joueur n'est pas l'hote on coupe le client
        if (isClientOnly)
            networkManager.StopClient();
        // si c'est l'hote on coupe client + serveur
        else
            networkManager.StopHost();
    }
}
