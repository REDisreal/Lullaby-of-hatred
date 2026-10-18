using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Player nhận " + amount + " sát thương, còn " + Mathf.Max(currentHealth, 0f) + " máu");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Player đã chết");
        Destroy(gameObject);
    }
}