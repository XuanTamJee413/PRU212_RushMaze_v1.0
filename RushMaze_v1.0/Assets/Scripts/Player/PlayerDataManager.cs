using UnityEngine;
using Assets.Data;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }
    public PlayerData PlayerData { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayerData = PlayerData.DefaultPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        PlayerData.Gold += amount;
    }
}
