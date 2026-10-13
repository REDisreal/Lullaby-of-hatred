using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private Sprite idleSprite;    // Sprite khi đứng yên
    [SerializeField] private Sprite moveSprite;    // Sprite khi di chuyển
    private Rigidbody2D rb;
    private SpriteRenderer SpriteRenderer;
    private InputAction MoveAction;
    private Vector2 MoveInput;
    private bool FacingRight=false;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0f;      // top-down nên không cần trọng lực
        rb.freezeRotation = true;  // tránh bị xoay khi va chạm
        MoveAction = InputSystem.actions.FindAction("Move");
    }
    private void Update()
    {
        // Composite 2DVector đã được chuẩn hóa nên đi chéo không bị nhanh hơn.
        // ClampMagnitude để chắc chắn khi trộn nhiều nguồn input.
        MoveInput = Vector2.ClampMagnitude(MoveAction.ReadValue<Vector2>(), 1f);
        if (MoveInput.x > 0 && !FacingRight) Flip();
        else if (MoveInput.x < 0 && FacingRight) Flip();
        // Xử lý đổi sprite dựa trên việc có di chuyển hay không
        if (MoveInput.sqrMagnitude > 0.01f)
        {
            if (moveSprite != null) SpriteRenderer.sprite = moveSprite;
        }
        else
        {
            if (idleSprite != null) SpriteRenderer.sprite = idleSprite;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = MoveInput * MoveSpeed;
    }
    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}