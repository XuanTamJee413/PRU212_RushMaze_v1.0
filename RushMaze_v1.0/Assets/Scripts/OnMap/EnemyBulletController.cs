using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 10f;
    [SerializeField] private float lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Gọi phương thức giảm máu của Player (nếu có)
            //other.GetComponent<>()?.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
