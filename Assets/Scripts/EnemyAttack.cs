using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private float nextAttackTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.time < nextAttackTime) return;
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;
        playerHealth.TakeDamage(damage);
        nextAttackTime = Time.time + attackCooldown;
    }
}