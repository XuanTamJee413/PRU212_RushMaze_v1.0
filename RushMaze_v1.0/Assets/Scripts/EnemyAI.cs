using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveInterval = 1.5f;
    private MazeGenerator mazeGenerator;
    private Vector2 currentDirection;

    void Start()
    {
        mazeGenerator = FindObjectOfType<MazeGenerator>();
        if (mazeGenerator == null)
        {
            Debug.LogError("⚠️ Không tìm thấy MazeGenerator!");
            return;
        }

        // Chọn hướng ban đầu ngẫu nhiên
        Vector2[] possibleDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        currentDirection = possibleDirections[Random.Range(0, possibleDirections.Length)];

        StartCoroutine(MoveContinuously());
    }

    IEnumerator MoveContinuously()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveInterval);
            TryMove();
        }
    }

    void TryMove()
    {
        Vector2 targetPos = (Vector2)transform.position + currentDirection;
        if (CanMoveTo(targetPos))
        {
            StartCoroutine(MoveToPosition(targetPos));
        }
        else
        {
            // Nếu gặp tường, đổi hướng
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        List<Vector2> validDirections = new List<Vector2>();

        foreach (var dir in directions)
        {
            Vector2 newPos = (Vector2)transform.position + dir;
            if (CanMoveTo(newPos))
            {
                validDirections.Add(dir);
            }
        }

        if (validDirections.Count > 0)
        {
            currentDirection = validDirections[Random.Range(0, validDirections.Count)];
        }
    }

    bool CanMoveTo(Vector2 pos)
    {
        int x = Mathf.RoundToInt(pos.x / mazeGenerator.pathWidth);
        int y = Mathf.RoundToInt(pos.y / mazeGenerator.pathWidth);

        int[,] maze = mazeGenerator.GetMaze();
        if (x >= 0 && x < mazeGenerator.mazeWidth && y >= 0 && y < mazeGenerator.mazeHeight)
        {
            return maze[x, y] == 0; // Chỉ đi vào đường đi
        }
        return false;
    }

    IEnumerator MoveToPosition(Vector2 targetPos)
    {
        Vector2 startPos = transform.position;
        float elapsedTime = 0f;
        float duration = 1f / moveSpeed;

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos; // Đảm bảo enemy ở đúng vị trí cuối
    }
}
