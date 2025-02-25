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

    // bi ban trung
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }
    // phuogn thuc hoi mau
    public void Healing(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    // cap nhat UI cho thanh mau
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
        gameObject.SetActive(false); // hide Player
    }
}
