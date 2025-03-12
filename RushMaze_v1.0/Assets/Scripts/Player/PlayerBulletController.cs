using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Data;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 20f;
    [SerializeField] private float bulletSpeed = 25f;
    [SerializeField] private float lifetime = 3f;
    private static Dictionary<GameObject, int> enemyHealth = new Dictionary<GameObject, int>();
    private const int DAMAGE_AMOUNT = 5;


    private UIManager uiManager;
    private void Start()
    {
        Destroy(gameObject, lifetime);
        StartCoroutine(RegenerateMana());
    }

    public void SetDirection(Vector2 direction)
    {
        uiManager = UIManager.Instance;
        PlayerData playerData = SaveSystem.LoadPlayer();

        if (playerData.CurrentMana < 5)
        {
            return;
        }
        else
        {
            uiManager.ModifyStats(mana: -5);

            playerData.CurrentMana -= 5;
            SaveSystem.SavePlayer(playerData);

            GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;

            if (!enemyHealth.ContainsKey(enemy))
            {
                int randomHealth = Random.Range(1, 4);
                enemyHealth[enemy] = randomHealth;
            }

            enemyHealth[enemy]--;

            if (enemyHealth[enemy] <= 0)
            {
                Destroy(enemy);
                enemyHealth.Remove(enemy);
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            PlayerData playerData = SaveSystem.LoadPlayer();
            if (playerData.CurrentMana < playerData.MaxMana)
            {
                playerData.CurrentMana = Mathf.Min(playerData.CurrentMana + 5, playerData.MaxMana);
                SaveSystem.SavePlayer(playerData);
            }
        }
    }
}
