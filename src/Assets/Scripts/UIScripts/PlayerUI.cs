using System;
using System.Collections;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerUI : NetworkBehaviour
{
    public static bool GameIsPaused;
    public static bool CanFilm = false;
    public Animator transitionAnimator;
    public GameObject pauseMenuUI;
    public GameObject hudUI;
    public GameObject camUI;

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
        else if (Input.GetKeyDown(KeyCode.E))
        {
            CanFilm = !CanFilm;
            transitionAnimator.SetTrigger("Fade");
        }
        // else if (Input.GetKeyDown(KeyCode.A))
        // {
        //     IEnumerator cor = Alert("yo", 5f);
        //     StartCoroutine(cor);
        // }
    }

    /// <summary>
    /// Affiche temporairemet le message donné en paramètre au joueur
    /// </summary>
    // public static IEnumerator Alert(string message, float displayTime)
    // {
    //  
    //     
    // }

    public void SwitchToCamMod()
    {
        camUI.SetActive(!camUI.activeSelf);
        hudUI.SetActive(!hudUI.activeSelf);
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
        Cursor.lockState = CursorLockMode.None;

        GameIsPaused = true;
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        // ssi le joueur n'est pas l'hote on coupe le client
        if (isClientOnly)
        {
            Debug.Log("Client coupé");
            networkManager.StopClient();
        }
        // si c'est l'hote on coupe client + serveur
        else
        {
            Debug.Log("Serveur coupé");
            networkManager.StopHost();
        }
    }
}
