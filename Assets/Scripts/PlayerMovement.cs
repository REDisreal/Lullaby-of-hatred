using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite moveSprite;

    private Rigidbody2D rb;
    private SpriteRenderer SpriteRenderer;
    private InputAction MoveAction;
    private Vector2 MoveInput;
    private bool FacingRight=false;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true; 
        MoveAction = InputSystem.actions.FindAction("Move");
    }
    private void Update()
    {
        MoveInput = Vector2.ClampMagnitude(MoveAction.ReadValue<Vector2>(), 1f);
        if (MoveInput.x > 0 && !FacingRight) Flip();
        else if (MoveInput.x < 0 && FacingRight) Flip();
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