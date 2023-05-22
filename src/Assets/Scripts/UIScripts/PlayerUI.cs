using System;
using System.Collections;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEditor;


public class PlayerUI : NetworkBehaviour
{
    public static bool GameIsPaused;
    public static bool CanFilm = false;
    public Animator transitionAnimator;
    public GameObject pauseMenuUI;
    public GameObject hudUI;
    public GameObject camUI;

    private NetworkManager networkManager;
    private static GameObject notification;

    [SerializeField]
    private GameObject tabMenu;

    [SerializeField] private GameObject itemsMenu;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            tabMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
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

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameIsPaused = true;
            itemsMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            Cursor.lockState = CursorLockMode.Locked;
            itemsMenu.SetActive(false);
            GameIsPaused = false;
        }
    }

    /// <summary>
    /// Affiche temporairemet le message donné en paramètre au joueur
    /// </summary>
    public static IEnumerator Notify(string message, float displayTime)
    {
        notification.SetActive(true);
        notification.GetComponent<TextMeshProUGUI>().text = message;
        yield return new WaitForSeconds(displayTime);
        notification.SetActive(false);
    }

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

    public void SetNotification(GameObject _notif)
    {
        notification = _notif;
    }
}
