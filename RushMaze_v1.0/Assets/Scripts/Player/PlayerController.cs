using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private GameObject bulletPrefab; // Gán Prefab của đạn
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera mainCamera;      // Camera chính

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        // Tính toán vị trí chuột trên thế giới
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - firePoint.position).normalized;

        // Tạo và bắn đạn
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Xoay đạn để bắn theo hướng chuột
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // Tính góc giữa vị trí chuột và người chơi
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));  // Xoay đạn theo góc tính được

        // Gán hướng di chuyển cho đạn
        bullet.GetComponent<PlayerBulletController>().SetDirection(direction);
    }

    private void HandleMovement()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed;

        if (playerInput.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (playerInput.x < 0) transform.localScale = new Vector3(-1, 1, 1);

        animator.SetBool("isRunning", playerInput != Vector2.zero);
    }
}
