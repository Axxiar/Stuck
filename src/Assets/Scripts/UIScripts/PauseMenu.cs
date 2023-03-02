using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        // on passe à la scene suivante (dans notre build, il faut que la scene suivant ce menu soit le jeu principal)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("quit!");
        Application.Quit();
    }
}
