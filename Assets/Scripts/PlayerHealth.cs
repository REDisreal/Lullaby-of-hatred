using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float regenPerSecond = 1f;

    private float currentHealth;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        Heal(regenPerSecond * Time.deltaTime);
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
    public float Heal(float amount)
    {
        float before = currentHealth;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        return currentHealth - before;
    }
    public void Die()
    {
        Debug.Log("Player đã chết");
        Destroy(gameObject);
    }
}