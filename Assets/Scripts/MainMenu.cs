using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public static bool isSinglePlayer = false;

    public void SinglePlayer()
    {
        isSinglePlayer = true;
    }

    public void MultiPlayer()
    {
        isSinglePlayer = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
