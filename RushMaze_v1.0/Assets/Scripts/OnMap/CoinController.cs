using UnityEngine;

public class CoinController : MonoBehaviour
{
    private AudioManager audioManager;
    private MazeGenerator mazeGenerator;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        mazeGenerator = FindObjectOfType<MazeGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDataManager.Instance.AddGold(100);
            Debug.Log("Nhặt coin! Gold hiện tại: " + PlayerDataManager.Instance.PlayerData.Gold);
            UIManager.Instance.ModifyStats(gold: 10);

            if (audioManager != null)
            {
                audioManager.PlayCoinSound();
            }

            // Gọi hàm CollectCoin() để kiểm tra điều kiện qua màn
            if (mazeGenerator != null)
            {
                mazeGenerator.CollectCoin();
            }

            Destroy(gameObject);
        }
    }
}
