using UnityEngine;
using Assets.Data; 
using System.IO; 

public class LobbyManager : MonoBehaviour
{
    private void Start()
    {
        
        PlayerData playerData = SaveSystem.LoadPlayer();
        Debug.Log($"[Lobby] Trước khi hồi phục: {playerData.CurrentHp}/{playerData.MaxHp} HP - {playerData.CurrentMana}/{playerData.MaxMana} Mana");

        
        playerData.CurrentHp = playerData.MaxHp;
        playerData.CurrentMana = playerData.MaxMana;

        
        SaveSystem.SavePlayer(playerData);

        
        string savePath = Application.dataPath + "/playerdata.json";
        string savedJson = File.ReadAllText(savePath);
        Debug.Log($"[Lobby] Dữ liệu sau khi lưu: {savedJson}");

        Debug.Log($"[Lobby] Sau khi hồi phục: {playerData.CurrentHp}/{playerData.MaxHp} HP - {playerData.CurrentMana}/{playerData.MaxMana} Mana");
    }
}
