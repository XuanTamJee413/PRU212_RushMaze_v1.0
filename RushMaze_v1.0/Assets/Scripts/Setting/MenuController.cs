using Assets.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private PlayerData playerData;
    public void PlayButton()
    {
        SceneManager.LoadScene("LobbyScene");
        LoadGame();
    }
    public void LoadGame()
    {
        playerData = SaveSystem.LoadPlayer();

        if (playerData != null)
        {
            Debug.Log($"Dữ liệu người chơi đã tải: HP: {playerData.CurrentHp}, Mana: {playerData.CurrentMana}, Gold: {playerData.Gold}, Key: {playerData.Key}");
        }
        else
        {
            Debug.LogWarning("LoadGame thất bại: Không tìm thấy dữ liệu hoặc dữ liệu bị lỗi!");
        }
    }
    public void SaveGame()
    {
        if (playerData != null)
        {
            SaveSystem.SavePlayer(playerData);
            Debug.Log("Game đã được lưu!");
        }
        else
        {
            Debug.LogWarning("⚠ Không thể lưu game! Dữ liệu nhân vật chưa được khởi tạo.");
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    public void OptionsButton()
    {
       // Debug.Log("Mở Cài Đặt: Âm thanh, đồ họa...");
        SceneManager.LoadScene("TestOptionScene");
    }

    public void StoryButton()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Thoát Game.");
    }
}
