using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tăng 100 Gold cho người chơi
            PlayerDataManager.Instance.AddGold(100);
            Debug.Log("Nhặt coin! Gold hiện tại: " + PlayerDataManager.Instance.PlayerData.Gold);

            // Hủy coin khi nhặt
            Destroy(gameObject);
        }
    }
}



