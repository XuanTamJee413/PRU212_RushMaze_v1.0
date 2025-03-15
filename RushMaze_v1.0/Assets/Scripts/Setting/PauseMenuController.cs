using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu; // Kéo thả PauseMenu 

    public void TogglePauseMenu()
    {
        bool isActive = pauseMenu.activeSelf;
        pauseMenu.SetActive(!isActive);

        Time.timeScale = isActive ? 1 : 0;
    }

    public void GoToLobbyScene()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("LobbyScene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
