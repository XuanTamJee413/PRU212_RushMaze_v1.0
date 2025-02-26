using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OptionsButton()
    {
       // Debug.Log("Mở Cài Đặt: Âm thanh, đồ họa...");
        SceneManager.LoadScene("TestOptionScene");
    }

    public void StoryButton()
    {
        Debug.Log("Hướng dẫn: Dùng phím mũi tên hoặc WASD để di chuyển.");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Thoát Game.");
    }
}
