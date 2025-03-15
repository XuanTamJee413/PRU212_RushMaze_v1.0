using UnityEngine;
using UnityEngine.UI;
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float maxHealth = 100f; 
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; 
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }
    public void Healing(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthSlider.value = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Player died!");
        gameObject.SetActive(false); 
    }
}
