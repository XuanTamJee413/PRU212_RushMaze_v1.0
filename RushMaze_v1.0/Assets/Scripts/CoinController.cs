using UnityEngine;
using System.Collections.Generic;

public class CoinController : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab của coin
    public int coinCount = 10; // Số lượng coin cần spawn
    public MazeGenerator mazeGenerator; // Tham chiếu tới MazeGenerator

    private List<Vector2> spawnPositions = new List<Vector2>();

    void Start()
    {
        // Kiểm tra nếu mazeGenerator chưa được gán
        if (mazeGenerator == null)
        {
            Debug.LogError("❌ MazeGenerator chưa được gán! Kéo MazeGenerator vào Inspector.");
            return;
        }

        // Kiểm tra nếu coinPrefab chưa được gán
        if (coinPrefab == null)
        {
            Debug.LogError("❌ coinPrefab chưa được gán! Kéo Prefab coin vào Inspector.");
            return;
        }

        GenerateCoinSpawns();
        SpawnCoins();
    }

    void GenerateCoinSpawns()
    {
        int[,] maze = mazeGenerator.GetMaze();
        if (maze == null)
        {
            Debug.LogError("❌ Maze chưa được tạo! Kiểm tra lại MazeGenerator.");
            return;
        }

        int width = mazeGenerator.mazeWidth;
        int height = mazeGenerator.mazeHeight;
        float pathWidth = mazeGenerator.pathWidth; // Lấy pathWidth để chỉnh vị trí spawn

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 0) // Chỉ spawn coin trên đường đi
                {
                    spawnPositions.Add(new Vector2(x, y));
                }
            }
        }

        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("⚠️ Không có vị trí hợp lệ để spawn coin!");
        }
        else
        {
            Debug.Log($"✅ Có {spawnPositions.Count} vị trí hợp lệ để spawn coin.");
        }
    }

    void SpawnCoins()
    {
        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("⚠️ Không thể spawn coin vì không có vị trí hợp lệ.");
            return;
        }

        System.Random rand = new System.Random();
        int spawnLimit = Mathf.Min(coinCount, spawnPositions.Count);

        for (int i = 0; i < spawnLimit; i++)
        {
            int index = rand.Next(spawnPositions.Count);
            Vector2 spawnPos = spawnPositions[index];
            spawnPositions.RemoveAt(index); // Xóa khỏi danh sách để tránh trùng vị trí

            // Tính toán vị trí spawn
            Vector3 worldPos = new Vector3(spawnPos.x * mazeGenerator.pathWidth + 0.5f,
                                           spawnPos.y * mazeGenerator.pathWidth + 0.5f,
                                           0);

            // Kiểm tra Prefab trước khi tạo
            if (coinPrefab == null)
            {
                Debug.LogError("❌ coinPrefab không hợp lệ!");
                return;
            }

            // Instantiate coin
            GameObject coin = Instantiate(coinPrefab, worldPos, Quaternion.identity);

            if (coin != null)
            {
                Debug.Log($"✅ Coin {coin.name} spawn tại {worldPos}");
                coin.SetActive(true); // Đảm bảo coin được kích hoạt
            }
            else
            {
                Debug.LogError("❌ Spawn coin thất bại!");
            }
        }
    }
}
