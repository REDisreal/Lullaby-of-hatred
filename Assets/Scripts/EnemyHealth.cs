using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 30f;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(name + " nhận " + amount + " sát thương, còn " + Mathf.Max(currentHealth, 0f) + " máu");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}