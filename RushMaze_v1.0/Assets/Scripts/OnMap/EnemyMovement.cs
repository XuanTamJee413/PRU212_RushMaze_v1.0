using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private LayerMask wallLayer; // Lớp tường để kiểm tra va chạm
    [SerializeField] private float wallDetectionDistance = 0.5f; // Khoảng cách kiểm tra tường

    private Vector2 moveDirection;

    private void Start()
    {
        ChooseNewDirection();
        StartCoroutine(ChangeDirectionRoutine());
    }

    private void Update()
    {
        Move();
        DetectWallAndTurn();
    }

    private void Move()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void DetectWallAndTurn()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, wallDetectionDistance, wallLayer);

        if (hit.collider != null)
        {
            ChooseNewDirection();
        }
    }

    private void ChooseNewDirection()
    {
        Vector2[] possibleDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        moveDirection = possibleDirections[Random.Range(0, possibleDirections.Length)];
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            ChooseNewDirection();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + moveDirection * wallDetectionDistance);
    }
}
