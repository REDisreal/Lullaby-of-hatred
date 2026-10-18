using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private bool spriteFacesRight = false;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
        FaceDirection(direction);
    }

    private void FaceDirection(Vector2 direction)
    {
        if (spriteRenderer == null) return;
        if (Mathf.Abs(direction.x) < 0.01f) return;
        bool playerIsOnRight = direction.x > 0f;
        spriteRenderer.flipX = playerIsOnRight != spriteFacesRight;
    }
}