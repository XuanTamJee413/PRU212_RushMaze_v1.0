using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;    // Prefab tường
    public GameObject floorPrefab;   // Prefab đường đi
    public GameObject playerPrefab;  // Prefab người chơi
    public GameObject monsterPrefab; // Prefab quái vật
    public GameObject coinPrefab;    // Prefab coin

    public float cellSize = 3.0f;    // Kích thước ô
    public float respawnTime = 10f;  // Thời gian hồi sinh quái

    private int width;
    private int height;
    private int monsterCount;
    private int coinCount;

    private int[,] maze;
    private List<Vector2> floorPositions = new List<Vector2>();

    void Start()
    {
        // Kiểm tra và gán giá trị mặc định nếu null hoặc không hợp lệ
        width = LevelData.MazeWidth > 0 ? LevelData.MazeWidth : 5;
        height = LevelData.MazeHeight > 0 ? LevelData.MazeHeight : 5;
        monsterCount = LevelData.MonsterCount > 0 ? LevelData.MonsterCount : 2;
        coinCount = LevelData.CoinCount > 0 ? LevelData.CoinCount : 2;

        Debug.Log($"Maze: {width}x{height}, Monsters: {monsterCount}, Coins: {coinCount}");

        GenerateMaze();
        SpawnPlayer();
        SpawnObjects(monsterPrefab, monsterCount, true);
        SpawnObjects(coinPrefab, coinCount, false);
    }


    void GenerateMaze()
    {
        maze = new int[width, height];

        // Khởi tạo toàn bộ thành tường
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1;
            }
        }

        // Tạo đường đi bằng thuật toán Recursive Backtracking
        RecursiveBacktracking(1, 1);
        DrawMaze();
    }

    void RecursiveBacktracking(int x, int y)
    {
        maze[x, y] = 0; // Đánh dấu là đường đi
        floorPositions.Add(new Vector2(x, y)); // Lưu vị trí đường đi

        int[] dx = { 0, 0, 2, -2 };
        int[] dy = { 2, -2, 0, 0 };
        Shuffle(dx, dy);

        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            if (nx > 0 && ny > 0 && nx < width - 1 && ny < height - 1 && maze[nx, ny] == 1)
            {
                maze[x + dx[i] / 2, y + dy[i] / 2] = 0; // Phá tường giữa
                RecursiveBacktracking(nx, ny);
            }
        }
    }

    void DrawMaze()
    {
        Debug.Log($"Tổng ô đường đi: {floorPositions.Count}");

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0);
                if (maze[x, y] == 1)
                {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(floorPrefab, position, Quaternion.identity);
                }
            }
        }
    }

    void SpawnPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player"); // Tìm player trong scene (theo tag)

        if (player != null)
        {
            Vector2 randomPos = floorPositions[Random.Range(0, floorPositions.Count)];
            Vector3 spawnPos = GetCenteredPosition(randomPos);
            player.transform.position = spawnPos; // Cập nhật vị trí của player
        }
        else
        {
            Debug.LogError("Không tìm thấy Player trong Scene!");
        }
    }


    void SpawnObjects(GameObject prefab, int count, bool isRespawnable)
    {
        Debug.Log($"Đang sinh {count} {prefab.name}");
        for (int i = 0; i < count; i++)
        {
            Vector2 randomPos = floorPositions[Random.Range(0, floorPositions.Count)];
            Vector3 spawnPos = GetCenteredPosition(randomPos);
            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);

            if (isRespawnable)
            {
                StartCoroutine(RespawnMonster(obj, spawnPos));
            }
        }
    }

    IEnumerator RespawnMonster(GameObject monster, Vector3 position)
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            if (monster == null)
            {
                monster = Instantiate(monsterPrefab, position, Quaternion.identity);
            }
        }
    }

    // Đảm bảo quái và coin ở chính giữa ô
    Vector3 GetCenteredPosition(Vector2 cell)
    {
        return new Vector3(cell.x * cellSize, cell.y * cellSize, 0);
    }


    void Shuffle(int[] dx, int[] dy)
    {
        for (int i = 0; i < dx.Length; i++)
        {
            int rand = Random.Range(0, dx.Length);
            (dx[i], dx[rand]) = (dx[rand], dx[i]);
            (dy[i], dy[rand]) = (dy[rand], dy[i]);
        }
    }
}
