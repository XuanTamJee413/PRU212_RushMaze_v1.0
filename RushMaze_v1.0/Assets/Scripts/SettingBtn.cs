using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingBtn : MonoBehaviour
{
    public GameObject SettingPanel; // Panel chứa các lựa chọn
    public Button mainMenuButton, lobbyMapButton, exitButton; // Các nút điều hướng

    private bool isMenuActive = false;

    void Start()
    {
        SettingPanel.SetActive(false); // Ẩn panel lúc đầu

        // Gán sự kiện cho các nút
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        lobbyMapButton.onClick.AddListener(LoadLobbyMap);
        exitButton.onClick.AddListener(CloseMenu);
    }

    void OnMouseDown() // Khi nhấp vào biểu tượng Setting
    {
        Debug.Log("Bạn đã nhấn vào Setting!");
        if (!isMenuActive) OpenMenu();
    }

    void OpenMenu()
    {
        SettingPanel.SetActive(true);
        isMenuActive = true;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Chuyển đến Main Menu
    }

    public void LoadLobbyMap()
    {
        SceneManager.LoadScene("LobbyMap"); // Chuyển đến Lobby Map
    }

    public void LoadLevelByName(string levelName)
    {
        SceneManager.LoadScene(levelName); // Load Scene theo tên
    }

    void CloseMenu()
    {
        SettingPanel.SetActive(false); // Đóng menu
        isMenuActive = false;
    }
}
