using UnityEngine;

public class KeyController : MonoBehaviour
{
    private AudioManager audioManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        if (other.CompareTag("Player"))
        {

            PlayerDataManager.Instance.AddKey();
            Debug.Log("Đã nhặt Key! " + PlayerDataManager.Instance.PlayerData.Key);


            Destroy(gameObject);
            audioManager.PlayCoinSound();
        }



    }
}
