using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CoinController : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount = 10;
    public MazeGenerator mazeGenerator;
    private List<Vector2> spawnPositions = new List<Vector2>();
    private List<GameObject> coins = new List<GameObject>();

    IEnumerator Start()
    {
        while (mazeGenerator == null || mazeGenerator.GetMaze() == null)
        {
            Debug.Log("Waiting for maze to be ready...");
            yield return null;
        }

        GenerateCoinSpawns();
        SpawnCoins();
    }


    void GenerateCoinSpawns()
    {
        int[,] maze = mazeGenerator.GetMaze();

        if (maze == null)
        {
            Debug.LogError("Maze data is null in CoinController!");
            return;
        }

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

        Debug.Log($"Generated {spawnPositions.Count} coin spawn positions.");
    }


    void SpawnCoins()
    {
        System.Random rand = new System.Random();
        int spawnLimit = Mathf.Min(coinCount, spawnPositions.Count);

        for (int i = 0; i < spawnLimit; i++)
        {
            int index = rand.Next(spawnPositions.Count);
            Vector2 spawnPos = spawnPositions[index];
            spawnPositions.RemoveAt(index);

            Vector3 worldPos = new Vector3(spawnPos.x * mazeGenerator.pathWidth, spawnPos.y * mazeGenerator.pathWidth, 0);
            GameObject coin = Instantiate(coinPrefab, worldPos, Quaternion.identity);
            coin.GetComponent<SpriteRenderer>().sortingLayerName = "Item"; 
            coin.AddComponent<CoinController>();
            coins.Add(coin);
        }
    }
}
