using System;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int mazeWidth = 15;
    public int mazeHeight = 15;
    public float pathWidth = 3.0f;
    public float wallWidth = 3.0f;
    public int centerSize = 3;

    public int exitX, exitY;

    public GameObject wallPrefab;
    public GameObject pathPrefab;
    public GameObject borderPrefab;
    public GameObject player;
    

    private int[,] maze;

    void Start()
    {
        GenerateMaze();
    }

    void PlacePlayerAtExit()
    {
        Vector3 playerPos = new Vector3(exitX * pathWidth, exitY * pathWidth, 0);
        player.transform.position = playerPos;
        Debug.Log($"Player Position: ({playerPos.x}, {playerPos.y}) - Exit: ({exitX}, {exitY})");
    }

    public void GenerateMaze()
    {
        maze = new int[mazeWidth, mazeHeight];
        System.Random rand = new System.Random();

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = 1;
            }
        }

        int startX = rand.Next(1, mazeWidth / 2) * 2;
        int startY = rand.Next(1, mazeHeight / 2) * 2;
        maze[startX, startY] = 0;
        CarvePath(startX, startY, rand);

        CreateExit(rand);
        BuildMaze();
        CreateBorder();
        PlacePlayerAtExit();
    }
   
    void CreateExit(System.Random rand)
    {
        int edge = rand.Next(4);

        switch (edge)
        {
            case 0: exitX = rand.Next(1, mazeWidth - 2); exitY = mazeHeight - 1; break;
            case 1: exitX = rand.Next(1, mazeWidth - 2); exitY = 0; break;
            case 2: exitX = 0; exitY = rand.Next(1, mazeHeight - 2); break;
            case 3: exitX = mazeWidth - 1; exitY = rand.Next(1, mazeHeight - 2); break;
        }

        maze[exitX, exitY] = 0;

        // Mở rộng thêm 9 ô xung quanh cổng ra
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                int nx = exitX + dx;
                int ny = exitY + dy;

                if (nx >= 0 && ny >= 0 && nx < mazeWidth && ny < mazeHeight)
                {
                    maze[nx, ny] = 0;
                }
            }
        }

        Debug.Log($"Exit: ({exitX}, {exitY})");
    }

    void CarvePath(int x, int y, System.Random rand)
    {
        int[] directions = { 0, 1, 2, 3 };
        for (int i = 0; i < directions.Length; i++)
        {
            int swap = rand.Next(i, directions.Length);
            (directions[i], directions[swap]) = (directions[swap], directions[i]);
        }

        foreach (var dir in directions)
        {
            int dx = 0, dy = 0;
            switch (dir)
            {
                case 0: dx = 2; break;
                case 1: dx = -2; break;
                case 2: dy = 2; break;
                case 3: dy = -2; break;
            }

            int nx = x + dx, ny = y + dy;

            if (nx >= 0 && ny >= 0 && nx < mazeWidth && ny < mazeHeight && maze[nx, ny] == 1)
            {
                maze[nx - dx / 2, ny - dy / 2] = 0;
                maze[nx, ny] = 0;
                CarvePath(nx, ny, rand);
            }
        }
    }

    void BuildMaze()
    {
        int centerX = mazeWidth / 2;
        int centerY = mazeHeight / 2;

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                if (Mathf.Abs(x - centerX) < centerSize / 2 && Mathf.Abs(y - centerY) < centerSize / 2)
                {
                    maze[x, y] = 0;
                }

                GameObject prefab = maze[x, y] == 1 ? wallPrefab : pathPrefab;

                float blockSize = maze[x, y] == 1 ? wallWidth : pathWidth;

                for (float offsetX = 0; offsetX < blockSize; offsetX += 1.0f)
                {
                    for (float offsetY = 0; offsetY < blockSize; offsetY += 1.0f)
                    {
                        Vector3 position = new Vector3(x * pathWidth + offsetX, y * pathWidth + offsetY, 0);
                        GameObject block = Instantiate(prefab, position, Quaternion.identity, transform);

                        // Nếu là cửa ra thì đổi màu thành vàng
                        if (x == exitX && y == exitY)
                        {
                            block.GetComponent<Renderer>().material.color = Color.yellow;
                        }
                    }
                }
            }
        }
    }

    public int[,] GetMaze()
    {
        return maze;
    }

    void CreateBorder()
    {
        float borderOffset = (wallWidth + pathWidth) / 2;

        // Tạo viền trên và dưới
        for (int x = -1; x <= mazeWidth; x++)
        {
            Vector3 bottomBorder = new Vector3(x * pathWidth, -borderOffset, 0);
            Vector3 topBorder = new Vector3(x * pathWidth, mazeHeight * pathWidth + borderOffset, 0);

            Instantiate(borderPrefab, bottomBorder, Quaternion.identity, transform);
            Instantiate(borderPrefab, topBorder, Quaternion.identity, transform);
        }

        // Tạo viền trái và phải
        for (int y = 0; y < mazeHeight; y++)
        {
            Vector3 leftBorder = new Vector3(-borderOffset, y * pathWidth, 0);
            Vector3 rightBorder = new Vector3(mazeWidth * pathWidth + borderOffset, y * pathWidth, 0);

            Instantiate(borderPrefab, leftBorder, Quaternion.identity, transform);
            Instantiate(borderPrefab, rightBorder, Quaternion.identity, transform);
        }
    }
    void Awake()
    {
        GenerateMaze(); 
    }




}
