using Assets.Data;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static string savePath = Application.dataPath + "/playerdata.json";


    public static void SavePlayer(PlayerData player)
    {
        string json = JsonUtility.ToJson(player, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Dữ liệu đã được lưu: " + savePath);
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }

        Debug.LogWarning("Không tìm thấy dữ liệu, tạo mới.");
        return PlayerData.DefaultPlayer();
    }

    public static bool HasSaveData() => File.Exists(savePath);

    public static void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Dữ liệu đã bị xóa!");
        }
    }
}