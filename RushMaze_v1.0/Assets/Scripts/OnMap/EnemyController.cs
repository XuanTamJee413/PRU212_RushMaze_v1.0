using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject bulletPrefab;   
    [SerializeField] private Transform firePoint;       
    [SerializeField] private float attackRange = 100f;    
    [SerializeField] private float fireRate = 1.5f;     

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

        if (distanceToPlayer <= attackRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        Vector2 shootDirection = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * 15f;

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); 
    }
}
