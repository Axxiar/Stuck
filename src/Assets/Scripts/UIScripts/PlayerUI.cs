using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEditor;
using UnityEngine.UI;


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
    public Sprite batterySprite;
    public List<GameObject> items;
    private void Start()
    {
        networkManager = NetworkManager.singleton;
    }
    
    public void Update()
    {
        if (!GameIsPaused)
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
            
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CanFilm = !CanFilm;
                transitionAnimator.SetTrigger("Fade");
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                itemsMenu.SetActive(true);
                Image image;
                for (int i = 0; i < Inventory.BatteriesCount; i++)
                {
                    image = items[i].GetComponent<Image>();
                    image.sprite = batterySprite;
                    var tempColor = image.color;
                    tempColor.a = 1f;
                    image.color = tempColor;
                }
            }
            else if (Input.GetKeyUp(KeyCode.I))
            {
                Image image;
                for (int i = 0; i < 5; i++)
                {
                    image = items[i].GetComponent<Image>();
                    image.sprite = null;
                    var tempColor = image.color;
                    tempColor.a = 0.168f;
                    image.color = tempColor;
                }
                itemsMenu.SetActive(false);
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                ResumeGame();
            else
                PauseGame();
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
        pauseMenuUI.SetActive(false);
        // pas opti mais plus safe :
        HideMenus();
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
        HideMenus();
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

    private void HideMenus()
    {
        //on cache le HUD
        hudUI.SetActive(false);
        itemsMenu.SetActive(false);
        tabMenu.SetActive(false);
        if (CanFilm)
            CanFilm = false;
        camUI.SetActive(false);
        
    }
}
