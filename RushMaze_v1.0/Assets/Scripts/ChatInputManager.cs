using Assets.Data;
using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChatInputManager : MonoBehaviour
{
    public GameObject chatbox;       
    public TMP_InputField chatInput; 

    private bool isChatOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isChatOpen)
            {
                ToggleChatbox();
            }
            else if (!string.IsNullOrWhiteSpace(chatInput.text))
            {
                ExecuteCommand(chatInput.text);
                chatInput.text = ""; 
                ToggleChatbox();    
            }
        }
    }


    void ToggleChatbox()
    {
        isChatOpen = !isChatOpen;

        chatInput.gameObject.SetActive(isChatOpen);

        if (isChatOpen)
        {
            chatInput.ActivateInputField(); 
        }
    }


    void ExecuteCommand(string command)
    {
        command = command.ToLower();

        if (command == "exit")
        {
            Application.Quit();
        }
        else if (command == "home")
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (command == "save")
        {
            SceneManager.LoadScene("MainMenu");
            MenuController menu = new MenuController();
            menu.SaveGame();
        }
        else if (command == "lob")
        {
            SceneManager.LoadScene("LobbyScene");
        }
        else if (command.StartsWith("l"))
        {
            if (int.TryParse(command.Substring(1), out int level) && level >= 1)
            {
                int size = 5 + (level - 1) * 2;
                int area = size * size;

                int monsterCount = Mathf.Max(2, area / 20);
                int coinCount = Mathf.Max(2, area / 20);

                LevelData.SetLevelData(size, size, monsterCount, coinCount);
                SceneManager.LoadScene("MazeScene");
            }
            else
            {
                Debug.Log($"Lệnh không hợp lệ: {command}");
                SceneManager.LoadScene("MainMenu"); 
            }
        }
        else
        {
            Debug.Log($"Lệnh không hợp lệ: {command}");
            SceneManager.LoadScene("MainMenu");
        }
    }


}
