using System;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("hello");
            if (GameIsPaused)
            {
                print("o");
                ResumeGame();
            }
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        //on cache le menu pause
        pauseMenuUI.SetActive(false);
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
        //on affiche le menu pause
        pauseMenuUI.SetActive(true);
        //on débloque le curseur
        Cursor.lockState = CursorLockMode.Confined;

        GameIsPaused = true;
    }
    public void QuitGame()
    {
        Debug.Log("quit!");
        Application.Quit();
    }
}
