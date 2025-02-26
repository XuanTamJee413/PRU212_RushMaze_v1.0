using UnityEngine;
using System.Collections.Generic;

public class CoinController : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount = 10;
    public MazeGenerator mazeGenerator;
    private List<Vector2> spawnPositions = new List<Vector2>();

    void Start()
    {
        // Kiểm tra mazeGenerator đã được gán chưa
        if (mazeGenerator == null)
        {
            Debug.LogError("MazeGenerator chưa được gán! Hãy kéo GameObject chứa MazeGenerator vào Inspector.");
            return;
        }

        // Kiểm tra coinPrefab đã được gán chưa
        if (coinPrefab == null)
        {
            Debug.LogError("coinPrefab chưa được gán! Hãy kéo Prefab coin vào Inspector.");
            return;
        }

        GenerateCoinSpawns();
        SpawnCoins();
    }

    void GenerateCoinSpawns()
    {
        int[,] maze = mazeGenerator.GetMaze();
        int width = mazeGenerator.mazeWidth;
        int height = mazeGenerator.mazeHeight;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 0) // Chỉ spawn trên đường đi
                {
                    spawnPositions.Add(new Vector2(x, y));
                }
            }
        }

        // Kiểm tra nếu không có vị trí nào hợp lệ
        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("⚠️ Không có vị trí hợp lệ để spawn coin!");
        }
        else
        {
            Debug.Log($"Có {spawnPositions.Count} vị trí hợp lệ để spawn coin.");
        }
    }

    void SpawnCoins()
    {
        System.Random rand = new System.Random();
        int spawnLimit = Mathf.Min(coinCount, spawnPositions.Count);

        if (spawnLimit == 0)
        {
            Debug.LogWarning("Không thể spawn coin vì không có vị trí hợp lệ.");
            return;
        }

        for (int i = 0; i < spawnLimit; i++)
        {
            int index = rand.Next(spawnPositions.Count);
            Vector2 spawnPos = spawnPositions[index];
            spawnPositions.RemoveAt(index); // Xóa khỏi danh sách để tránh trùng vị trí

            Vector3 worldPos = new Vector3(spawnPos.x * mazeGenerator.pathWidth, spawnPos.y * mazeGenerator.pathWidth, 0);
            GameObject coin = Instantiate(coinPrefab, worldPos, Quaternion.identity);

            // Kiểm tra xem coin có spawn thành công không
            if (coin != null)
            {
                Debug.Log($"Coin {coin.name} spawn tại {worldPos}");
            }
            else
            {
                Debug.LogError("Spawn coin thất bại!");
            }
        }
    }
}
