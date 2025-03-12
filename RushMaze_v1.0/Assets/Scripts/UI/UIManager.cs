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

    public Image hpBar;
    public Image mpBar;

    private void Start()
    {
        LoadAndDisplay();
    }

    // Tải và hiển thị dữ liệu nhân vật
    void LoadAndDisplay()
    {
        PlayerData player = SaveSystem.LoadPlayer();

        UpdateUI(player);
    }

    // Cập nhật UI sau khi thay đổi dữ liệu
    public void UpdateUI(PlayerData player)
    {
        hpText.text = $"HP: {player.CurrentHp}/{player.MaxHp}";
        manaText.text = $"Mana: {player.CurrentMana}/{player.MaxMana}";
        goldText.text = $"Gold: {player.Gold}";
        powerText.text = $"Power: {player.Power}";
        expText.text = $"Exp: {player.Exp}";
        dameText.text = $"Dame Per Hit: {player.Dame}";
        levelText.text = $"Level: {player.Level}";

        hpBar.fillAmount = (float)player.CurrentHp / player.MaxHp;
        mpBar.fillAmount = (float)player.CurrentMana / player.MaxMana;
    }
}
