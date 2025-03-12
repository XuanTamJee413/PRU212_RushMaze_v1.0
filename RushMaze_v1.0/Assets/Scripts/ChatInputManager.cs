using Assets.Data;
using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChatInputManager : MonoBehaviour
{
    public GameObject chatbox;       // Canvas chứa InputField
    public TMP_InputField chatInput; // TMP InputField

    private bool isChatOpen = false; // Trạng thái hộp chat

    void Update()
    {
        // Bật/tắt chatbox khi nhấn Enter (chỉ mở nếu đang tắt)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isChatOpen)
            {
                ToggleChatbox();
            }
            else if (!string.IsNullOrWhiteSpace(chatInput.text))
            {
                ExecuteCommand(chatInput.text);
                chatInput.text = ""; // Xóa nội dung sau khi nhập lệnh
                ToggleChatbox();    // Tắt chatbox
            }
        }
    }


    void ToggleChatbox()
    {
        isChatOpen = !isChatOpen;

        // Chỉ ẩn InputField thay vì tắt Canvas
        chatInput.gameObject.SetActive(isChatOpen);

        if (isChatOpen)
        {
            chatInput.ActivateInputField(); // Focus vào InputField
        }
    }


    // Xử lý lệnh
    void ExecuteCommand(string command)
    {
        command = command.ToLower();

        if (command == "exit")
        {
            Application.Quit(); // Thoát game
        }
        else if (command == "home")
        {
            SceneManager.LoadScene("MainMenu"); // Về MainMenu
        }else if (command == "save")
        {
            SceneManager.LoadScene("MainMenu"); // Về MainMenu
            MenuController menu = new MenuController();
            menu.SaveGame();
        }
        else if (command == "lob")
        {
            SceneManager.LoadScene("LobbyScene"); // Về LobbyScene
        }
        else if (command.StartsWith("l"))
        {
            if (int.TryParse(command.Substring(1), out int level) && level >= 1)
            {
                int size = 5 + (level - 1) * 2; // Tăng kích thước mê cung
                int area = size * size;         // Diện tích mê cung

                int monsterCount = Mathf.Max(2, area / 20); // 1 quái mỗi 20 ô
                int coinCount = Mathf.Max(2, area / 20);    // 1 coin mỗi 20 ô

                LevelData.SetLevelData(size, size, monsterCount, coinCount);
                SceneManager.LoadScene("MazeScene");
            }
            else
            {
                Debug.Log($"Lệnh không hợp lệ: {command}");
                SceneManager.LoadScene("MainMenu"); // Về Home nếu sai
            }
        }
        else
        {
            Debug.Log($"Lệnh không hợp lệ: {command}");
            SceneManager.LoadScene("MainMenu"); // Về Home nếu lệnh sai
        }
    }


}
