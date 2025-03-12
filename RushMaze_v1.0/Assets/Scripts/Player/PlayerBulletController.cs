using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 20f; // Sát thương đạn
    [SerializeField] private float bulletSpeed = 25f;  // Tốc độ đạn
    [SerializeField] private float lifetime = 3f;      // Thời gian tồn tại

    private void Start()
    {
        Destroy(gameObject, lifetime); // Tự hủy sau 'lifetime' giây
    }

    public void SetDirection(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Giảm máu quái (nếu có hàm TakeDamage())
            //other.GetComponent<EnemyController>()?.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
