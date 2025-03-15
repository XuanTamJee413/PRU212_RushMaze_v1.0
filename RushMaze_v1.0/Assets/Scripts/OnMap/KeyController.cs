using UnityEngine;

public class KeyController : MonoBehaviour
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
            // Cập nhật dữ liệu người chơi
            PlayerDataManager.Instance.AddKey();
            Debug.Log("Đã nhặt Key! " + PlayerDataManager.Instance.PlayerData.Key);
            UIManager.Instance.ModifyStats(key: 1);

            // Gọi âm thanh nếu tồn tại
            if (audioManager != null)
            {
                audioManager.PlayCoinSound();
            }

            // Kiểm tra điều kiện qua màn
            if (mazeGenerator != null)
            {
                mazeGenerator.CollectKey();
            }

            // Hủy vật phẩm sau khi nhặt
            Destroy(gameObject);
        }
    }
}
