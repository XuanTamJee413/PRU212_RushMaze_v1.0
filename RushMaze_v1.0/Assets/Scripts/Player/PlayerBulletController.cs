using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Data;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 20f;
    [SerializeField] private float bulletSpeed = 25f;
    [SerializeField] private float lifetime = 3f;
    private static Dictionary<GameObject, int> enemyHealth = new Dictionary<GameObject, int>();

    private void Start()
    {
        Destroy(gameObject, lifetime);
        StartCoroutine(RegenerateMana()); 
    }

    public void SetDirection(Vector2 direction)
    {
        
        PlayerData playerData = SaveSystem.LoadPlayer();

        
        if (playerData.CurrentMana <= 0)
        {
            Debug.Log("[Player] Mana đã hết, không thể bắn!");
            return;
        }

        
        playerData.CurrentMana -= 5;
        SaveSystem.SavePlayer(playerData);
        Debug.Log($"[Player] Đã bắn! Mana còn lại: {playerData.CurrentMana}/{playerData.MaxMana}");

        
        GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
        Debug.Log($"[Bullet] Đạn bắn ra theo hướng {direction} với tốc độ {bulletSpeed}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;

            if (!enemyHealth.ContainsKey(enemy))
            {
                int randomHealth = Random.Range(1, 4);
                enemyHealth[enemy] = randomHealth;
                Debug.Log($"[Enemy] Quái {enemy.name} có {randomHealth} máu.");
            }

            enemyHealth[enemy]--;
            Debug.Log($"[Bullet] Đạn chạm vào quái: {enemy.name}. Máu còn lại: {enemyHealth[enemy]}");

            if (enemyHealth[enemy] <= 0)
            {
                Destroy(enemy);
                Debug.Log($"[Enemy] Quái {enemy.name} đã bị tiêu diệt!");
                enemyHealth.Remove(enemy);
            }

            Destroy(gameObject);
            Debug.Log("[Bullet] Viên đạn bị hủy sau khi va chạm.");
        }
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            PlayerData playerData = SaveSystem.LoadPlayer();
            if (playerData.CurrentMana < playerData.MaxMana)
            {
                playerData.CurrentMana = Mathf.Min(playerData.CurrentMana + 5, playerData.MaxMana);
                SaveSystem.SavePlayer(playerData);
                Debug.Log($"[Player] Hồi phục 5 mana! Mana hiện tại: {playerData.CurrentMana}/{playerData.MaxMana}");
            }
        }
    }
}
