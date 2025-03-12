using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CoinController : MonoBehaviour
{
    private AudioManager audioManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        if (other.CompareTag("Player"))
        {
            
            PlayerDataManager.Instance.AddGold(100);
            Debug.Log("Nhặt coin! Gold hiện tại: " + PlayerDataManager.Instance.PlayerData.Gold);

            
            Destroy(gameObject);
            audioManager.PlayCoinSound();
        }


        
    }
}


