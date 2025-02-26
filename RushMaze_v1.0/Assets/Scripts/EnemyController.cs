using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 15;
    public MazeGenerator mazeGenerator;
    private List<Vector2> spawnPositions = new List<Vector2>();
    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        if (mazeGenerator == null)
        {
            Debug.LogError("MazeGenerator chưa được gán! Hãy kéo GameObject chứa MazeGenerator vào Inspector.");
            return;
        }

        if (enemyPrefab == null)
        {
            Debug.LogError("EnemyPrefab chưa được gán! Hãy kéo Prefab quái vào Inspector.");
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
                if (maze[x, y] == 0)
                {
                    Vector2 pos = new Vector2(x, y);
                    if (Vector2.Distance(pos, exitPos) > 2)
                    {
                        spawnPositions.Add(pos);
                    }
                }
            }
        }

        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("⚠️ Không có vị trí hợp lệ để spawn quái!");
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
            spawnPositions.RemoveAt(index);

            Vector3 worldPos = new Vector3(spawnPos.x * mazeGenerator.pathWidth, spawnPos.y * mazeGenerator.pathWidth, 0);
            GameObject enemy = Instantiate(enemyPrefab, worldPos, Quaternion.identity);
            enemies.Add(enemy);

            if (enemy.GetComponent<NavMeshAgent>() == null)
            {
                enemy.AddComponent<NavMeshAgent>(); // Đảm bảo có NavMeshAgent
            }

            enemy.GetComponent<NavMeshAgent>().speed = 2.0f;
            enemy.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";

            Debug.Log($"Quái spawn tại {worldPos}");
        }

        InvokeRepeating("MoveEnemies", 1f, 3f);
    }

    
}
