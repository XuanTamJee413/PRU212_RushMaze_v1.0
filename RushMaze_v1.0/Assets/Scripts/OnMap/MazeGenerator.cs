using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;
using System.Drawing;
using UnityEngine.SceneManagement;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject playerPrefab;
    public GameObject monsterPrefab;
    public GameObject coinPrefab;
    public GameObject keyPrefab; 

    public float cellSize = 3.0f;
    public float respawnTime = 15f;

    private int width;
    private int height;
    private int monsterCount;
    private int coinCount;

    private int[,] maze;
    private List<Vector2> floorPositions = new List<Vector2>();
    private Vector2 exitPosition;

    private bool allCoinsCollected = false;
    private bool keyCollected = false;
    private int coinsCollectedCount = 0;

    void Start()
    {
        width = LevelData.MazeWidth > 0 ? LevelData.MazeWidth : 5;
        height = LevelData.MazeHeight > 0 ? LevelData.MazeHeight : 5;
        monsterCount = LevelData.MonsterCount > 0 ? LevelData.MonsterCount : 2;
        coinCount = LevelData.CoinCount > 0 ? LevelData.CoinCount : 2;

        Debug.Log($"Maze: {width}x{height}, Monsters: {monsterCount}, Coins: {coinCount}");

        GenerateMaze();
        SpawnPlayer();
        SpawnObjects(monsterPrefab, monsterCount, true);
        SpawnObjects(coinPrefab, coinCount, false);
        SpawnKey();
    }

    void GenerateMaze()
    {
        maze = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1;
            }
        }

        RecursiveBacktracking(1, 1);
        exitPosition = new Vector2(width - 2, height - 2);
        DrawMaze();
    }

    void RecursiveBacktracking(int x, int y)
    {
        maze[x, y] = 0;
        floorPositions.Add(new Vector2(x, y));

        int[] dx = { 0, 0, 2, -2 };
        int[] dy = { 2, -2, 0, 0 };
        Shuffle(dx, dy);

        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            if (nx > 0 && ny > 0 && nx < width - 1 && ny < height - 1 && maze[nx, ny] == 1)
            {
                maze[x + dx[i] / 2, y + dy[i] / 2] = 0;
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
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Vector2 randomPos = floorPositions[Random.Range(0, floorPositions.Count)];
            Vector3 spawnPos = GetCenteredPosition(randomPos);
            player.transform.position = spawnPos;
        }
        else
        {
            Debug.LogError("Không tìm thấy Player trong Scene!");
        }
    }

    void SpawnObjects(GameObject prefab, int count, bool isRespawnable)
    {
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

    void SpawnKey()
    {
        Vector2 keyPosition = GetFarthestPosition(exitPosition);
        Vector3 spawnPos = GetCenteredPosition(keyPosition);
        Instantiate(keyPrefab, spawnPos, Quaternion.identity);
        Debug.Log("Chìa khóa sinh ra tại: " + keyPosition);
    }

    Vector2 GetFarthestPosition(Vector2 reference)
    {
        Vector2 farthest = floorPositions[0];
        float maxDistance = 0;

        foreach (var pos in floorPositions)
        {
            float distance = Vector2.Distance(pos, reference);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthest = pos;
            }
        }
        return farthest;
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

    public void CollectCoin()
    {
        coinsCollectedCount++;
        if (coinsCollectedCount >= coinCount)
        {
            allCoinsCollected = true;
            TryToAdvanceLevel();
        }
    }

    public void CollectKey()
    {
        keyCollected = true;
        TryToAdvanceLevel();
        Debug.Log("đã nhặt keyyyyyy");
    }
    void TryToAdvanceLevel()
    {
        if (allCoinsCollected && keyCollected)
        {
            LevelData.SetLevelData(width +3 , height+3, monsterCount+ 2, coinCount+ 2);
            SceneManager.LoadScene("MazeScene");
        }
    }
}
