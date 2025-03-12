using UnityEngine;
using UnityEngine.SceneManagement; // Thêm thư viện quản lý Scene
using Assets.Data; // Đảm bảo đúng namespace của PlayerData & SaveSystem

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 10f;
    [SerializeField] private float lifetime = 2f;
    private const int DAMAGE_AMOUNT = 5; // Mỗi viên đạn gây 5 sát thương

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerData playerData = SaveSystem.LoadPlayer();
            Debug.Log($"[Bullet] Đạn trúng người chơi! Máu trước khi trúng: {playerData.CurrentHp}/{playerData.MaxHp}");

            
            playerData.CurrentHp -= DAMAGE_AMOUNT;
            if (playerData.CurrentHp <= 0)
            {
                Debug.Log("[Player] Máu về 0! Chuyển về Lobby Scene...");
                SceneManager.LoadScene("LobbyScene"); 
                return; 
            }

            
            Debug.Log($"[Player] Máu còn lại: {playerData.CurrentHp}/{playerData.MaxHp}");
            SaveSystem.SavePlayer(playerData);

            
            Destroy(gameObject);
            Debug.Log("[Bullet] Viên đạn bị hủy sau khi va chạm.");
        }
    }
}
