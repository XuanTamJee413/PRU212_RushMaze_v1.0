using UnityEngine;
using System.Collections.Generic;

public class FireTrapController : MonoBehaviour
{
    public GameObject firetrapPrefab;  // Prefab của bẫy lửa
    public int trapCount = 10;      // Số lượng bẫy muốn đặt
    public MazeGenerator mazeGenerator;

    private List<Vector2> trapPositions = new List<Vector2>();

    void Start()
    {
        if (mazeGenerator == null)
        {
            Debug.LogError("MazeGenerator chưa được gán!");
            return;
        }

        if (firetrapPrefab == null)
        {
            Debug.LogError("trapPrefab chưa được gán!");
            return;
        }

        GenerateTrapSpawns();
        SpawnTraps();
    }

    void GenerateTrapSpawns()
    {
        int[,] maze = mazeGenerator.GetMaze();
        int width = mazeGenerator.mazeWidth;
        int height = mazeGenerator.mazeHeight;

        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                if (maze[x, y] == 1) // Chỉ spawn trên tường
                {
                    // Kiểm tra có đường đi bên cạnh không để đảm bảo bẫy gắn trên tường
                    if (maze[x + 1, y] == 0 || maze[x - 1, y] == 0 || maze[x, y + 1] == 0 || maze[x, y - 1] == 0)
                    {
                        trapPositions.Add(new Vector2(x, y));
                    }
                }
            }
        }

        if (trapPositions.Count == 0)
        {
            Debug.LogWarning("Không có vị trí hợp lệ để đặt bẫy!");
        }
    }

    void SpawnTraps()
    {
        System.Random rand = new System.Random();
        int spawnLimit = Mathf.Min(trapCount, trapPositions.Count);

        if (spawnLimit == 0)
        {
            Debug.LogWarning("Không thể đặt bẫy vì không có vị trí hợp lệ.");
            return;
        }

        for (int i = 0; i < spawnLimit; i++)
        {
            int index = rand.Next(trapPositions.Count);
            Vector2 spawnPos = trapPositions[index];
            trapPositions.RemoveAt(index); // Xóa vị trí đã sử dụng

            Vector3 worldPos = new Vector3(spawnPos.x * mazeGenerator.pathWidth, spawnPos.y * mazeGenerator.pathWidth, 0);
            GameObject trap = Instantiate(firetrapPrefab, worldPos, Quaternion.identity);

            // Định hướng bẫy dựa vào vị trí của nó trong mê cung
            OrientTrap(trap, (int)spawnPos.x, (int)spawnPos.y, mazeGenerator.GetMaze());

            if (trap != null)
            {
                Debug.Log($"Bẫy spawn tại {worldPos}");
            }
            else
            {
                Debug.LogError("Spawn bẫy thất bại!");
            }
        }
    }

    void OrientTrap(GameObject trap, int x, int y, int[,] maze)
    {
        // Kiểm tra hướng trống (nơi có đường đi)
        if (maze[x + 1, y] == 0)
        {
            trap.transform.rotation = Quaternion.Euler(0, 0, 180); // Quay bẫy về bên trái
        }
        else if (maze[x - 1, y] == 0)
        {
            trap.transform.rotation = Quaternion.Euler(0, 0, 0); // Hướng về bên phải
        }
        else if (maze[x, y + 1] == 0)
        {
            trap.transform.rotation = Quaternion.Euler(0, 0, -90); // Hướng xuống
        }
        else if (maze[x, y - 1] == 0)
        {
            trap.transform.rotation = Quaternion.Euler(0, 0, 90); // Hướng lên
        }
    }
}
