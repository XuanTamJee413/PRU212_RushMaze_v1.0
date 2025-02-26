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


    public GameObject goldPrefab; 
    private int clickCount = 0;
    private float clickTime = 0f;
    private float clickThreshold = 1f; 
    private float dropChance = 1f; 

    void OnMouseDown()
    {
        if (Time.time - clickTime > clickThreshold)
        {
            clickCount = 0; 
        }

        clickTime = Time.time;
        clickCount++;

        if (clickCount >= 3)
        {
            Destroy(gameObject);
            TryDropGold();
        }
    }

    void TryDropGold()
    {
        if (goldPrefab != null && Random.value < dropChance)
        {
            Instantiate(goldPrefab, transform.position, Quaternion.identity);
        }
    }


    void Start()
    {
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
    }

    void SpawnEnemies()
    {
        System.Random rand = new System.Random();
        int spawnLimit = Mathf.Min(enemyCount, spawnPositions.Count);

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
                enemy.AddComponent<NavMeshAgent>(); 
            }

            enemy.GetComponent<NavMeshAgent>().speed = 2.0f;


            enemy.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
            
            enemy.AddComponent<EnemyController>();

            //GameObject gold = Instantiate(goldPrefab, enemy.transform.position, Quaternion.identity);
            //gold.transform.SetParent(enemy.transform);

            
        }

    }


}