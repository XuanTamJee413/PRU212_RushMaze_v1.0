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
    public TMP_Text levelText;


    private void Start()
    {
        LoadAndDisplay();
    }

    // Tải và hiển thị dữ liệu nhân vật
    void LoadAndDisplay()
    {
        PlayerData player = SaveSystem.LoadPlayer();

        hpText.text = $"HP: {player.CurrentHp}/{player.MaxHp}";
        manaText.text = $"Mana: {player.CurrentMana}/{player.MaxMana}";
        goldText.text = $"Gold: {player.Gold}";
        powerText.text = $"Power: {player.Power}";
        expText.text = $"Exp: {player.Experience}";
        levelText.text = $"Level: {player.Level}";
    }

    // Cập nhật UI sau khi thay đổi dữ liệu
    public void UpdateUI(PlayerData player)
    {
        hpText.text = $"HP: {player.CurrentHp}/{player.MaxHp}";
        manaText.text = $"Mana: {player.CurrentMana}/{player.MaxMana}";
        goldText.text = $"Gold: {player.Gold}";
        powerText.text = $"Power: {player.Power}";
        expText.text = $"Exp: {player.Experience}";
        levelText.text = $"Level: {player.Level}";
    }
}
