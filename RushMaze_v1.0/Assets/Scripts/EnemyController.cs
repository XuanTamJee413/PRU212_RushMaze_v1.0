using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 5;
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

        // Kiểm tra enemyPrefab đã được gán chưa
        if (enemyPrefab == null)
        {
            Debug.LogError(" enemyPrefab chưa được gán! Hãy kéo Prefab quái vào Inspector.");
            return;
        }

        GenerateEnemySpawns();
        SpawnEnemies();
    }

    void GenerateEnemySpawns()
    {
        int[,] maze = mazeGenerator.GetMaze();
        int width = mazeGenerator.mazeWidth;
        int height = mazeGenerator.mazeHeight;
        Vector2 exitPos = new Vector2(mazeGenerator.exitX, mazeGenerator.exitY);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 0) // Chỉ spawn trên đường đi
                {
                    Vector2 pos = new Vector2(x, y);
                    if (Vector2.Distance(pos, exitPos) > 2) // Tránh spawn gần cửa ra
                    {
                        spawnPositions.Add(pos);
                    }
                }
            }
        }

        // Kiểm tra nếu không có vị trí nào hợp lệ
        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("⚠️ Không có vị trí hợp lệ để spawn quái!");
        }
        else
        {
            Debug.Log($" Có {spawnPositions.Count} vị trí hợp lệ để spawn quái.");
        }
    }

    void SpawnEnemies()
    {
        System.Random rand = new System.Random();
        int spawnLimit = Mathf.Min(enemyCount, spawnPositions.Count);

        if (spawnLimit == 0)
        {
            Debug.LogWarning("Không thể spawn quái vì không có vị trí hợp lệ.");
            return;
        }

        for (int i = 0; i < spawnLimit; i++)
        {
            int index = rand.Next(spawnPositions.Count);
            Vector2 spawnPos = spawnPositions[index];
            spawnPositions.RemoveAt(index); // Xóa khỏi danh sách để tránh trùng vị trí

            Vector3 worldPos = new Vector3(spawnPos.x * mazeGenerator.pathWidth, spawnPos.y * mazeGenerator.pathWidth, 0);
            GameObject enemy = Instantiate(enemyPrefab, worldPos, Quaternion.identity);

            // Kiểm tra xem quái có spawn thành công không
            if (enemy != null)
            {
                Debug.Log($"Quái {enemy.name} spawn tại {worldPos}");
            }
            else
            {
                Debug.LogError(" Spawn quái thất bại!");
            }
        }
    }
}
