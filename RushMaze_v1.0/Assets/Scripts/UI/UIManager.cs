using Assets.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text hpText;
    public TMP_Text manaText;
    public TMP_Text goldText;
    public TMP_Text powerText;
    public TMP_Text expText;
    public TMP_Text dameText;
    public TMP_Text levelText;
    public TMP_Text keyText;
    public Image hpBar;
    public Image mpBar;

    private static UIManager _instance;
    public static UIManager Instance => _instance;

    private PlayerData playerData;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadAndDisplay();
    }

    void LoadAndDisplay()
    {
        playerData = SaveSystem.LoadPlayer();
        playerData.CurrentMana =  playerData.MaxMana;
        playerData.CurrentHp =  playerData.MaxHp;
        UpdateUI(playerData);
    }

    public void UpdateUI(PlayerData player)
    {
        hpText.text = $"HP: {player.CurrentHp}/{player.MaxHp}";
        manaText.text = $"Mana: {player.CurrentMana}/{player.MaxMana}";
        goldText.text = $"Gold: {player.Gold}";
        powerText.text = $"Power: {player.Power}";
        expText.text = $"Exp: {player.Exp}";
        dameText.text = $"Dame Per Hit: {player.Dame}";
        levelText.text = $"Level: {player.Level}";
        keyText.text = $"Key: {player.Key}";

        hpBar.fillAmount = (float)player.CurrentHp / player.MaxHp;
        mpBar.fillAmount = (float)player.CurrentMana / player.MaxMana;
    }

    public void ModifyStats(int gold = 0, int hp = 0, int mana = 0, int exp = 0, int key = 0)
    {
        if (playerData == null)
        {
            Debug.LogWarning(" Không có dữ liệu nhân vật! Gọi LoadAndDisplay() trước.");
            return;
        }

        Debug.Log($" Trước khi cập nhật: Gold={playerData.Gold}, HP={playerData.CurrentHp}, Key={playerData.Key}");

        playerData.Gold += gold;
        playerData.CurrentHp = Mathf.Clamp(playerData.CurrentHp + hp, 0, playerData.MaxHp);
        playerData.CurrentMana = Mathf.Clamp(playerData.CurrentMana + mana, 0, playerData.MaxMana);
        playerData.Exp += exp;
        playerData.Key += key;

        Debug.Log($" Sau khi cập nhật: Gold={playerData.Gold}, HP={playerData.CurrentHp}, Key={playerData.Key}");

        UpdateUI(playerData);

        SaveSystem.SavePlayer(playerData);
    }

}
