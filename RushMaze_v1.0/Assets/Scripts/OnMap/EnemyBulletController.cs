using UnityEngine;
using UnityEngine.SceneManagement; // Thêm thư viện quản lý Scene
using Assets.Data; // Đảm bảo đúng namespace của PlayerData & SaveSystem

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 10f;
    [SerializeField] private float lifetime = 2f;
    private const int DAMAGE_AMOUNT = 5; // Mỗi viên đạn gây 5 sát thương
    private UIManager uiManager;



    private void Start()
    {
        Destroy(gameObject, lifetime);
        uiManager = UIManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerData playerData = SaveSystem.LoadPlayer();
            uiManager.ModifyStats(hp: -DAMAGE_AMOUNT);

            playerData.CurrentHp -= DAMAGE_AMOUNT;
            if (playerData.CurrentHp <= 0)
            {
                ShowDeathMenu();
                //SceneManager.LoadScene("LobbyScene");
                //return; 
            }
            
            SaveSystem.SavePlayer(playerData);
            Destroy(gameObject);
        }
    }
    private void ShowDeathMenu()
    {
        GameObject pauseMenu = System.Array.Find(Resources.FindObjectsOfTypeAll<GameObject>(), obj => obj.name == "PauseMenuPanel");
        GameObject ResumeBtn = System.Array.Find(Resources.FindObjectsOfTypeAll<GameObject>(), obj => obj.name == "ResumeBtn");

        if (pauseMenu != null)
        {
            bool isActive = pauseMenu.activeSelf;
            pauseMenu.SetActive(!isActive);
            ResumeBtn.SetActive(isActive);
            Time.timeScale = isActive ? 1 : 0;
        }
        else
        {
            Debug.Log("không thấy pause menu panel nào");
        }
    }
}
