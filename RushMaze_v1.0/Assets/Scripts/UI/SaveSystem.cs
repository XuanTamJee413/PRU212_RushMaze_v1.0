using Assets.Data;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static string savePath => Application.persistentDataPath + "/playerdata.json";

    // Lưu dữ liệu nhân vật vào JSON
    public static void SavePlayer(PlayerData player)
    {
        string json = JsonUtility.ToJson(player, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Dữ liệu đã được lưu: " + savePath);
    }

    // Tải dữ liệu nhân vật từ JSON
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

    // Kiểm tra tồn tại dữ liệu
    public static bool HasSaveData() => File.Exists(savePath);

    // Xóa dữ liệu
    public static void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Dữ liệu đã bị xóa!");
        }
    }
}
