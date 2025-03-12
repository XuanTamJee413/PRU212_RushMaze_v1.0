using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject bulletPrefab;   // Prefab của viên đạn
    [SerializeField] private Transform firePoint;       // Vị trí bắn
    [SerializeField] private float attackRange = 100f;    // Phạm vi bắn
    [SerializeField] private float fireRate = 1.5f;     // Thời gian hồi chiêu

    private Transform player;
    private float nextFireTime = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Nếu người chơi trong phạm vi và đã đủ thời gian hồi chiêu thì bắn
        if (distanceToPlayer <= attackRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        // Tính hướng bắn về phía người chơi
        Vector2 shootDirection = (player.position - firePoint.position).normalized;

        // Tạo viên đạn và đặt hướng di chuyển
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * 15f;// chỉnh tốc độ đạn

        // Xoay viên đạn theo hướng bắn
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Vẽ vùng bắn
    }
}
