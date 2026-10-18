using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private enum State
    {
        Waiting,
        Attacking
    }
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private float hitboxDuration = 0.2f;
    [SerializeField] private float hitboxWidth = 1.5f;
    [SerializeField] private float hitboxLength = 1.2f;
    [SerializeField] private float hitboxDistance = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject hitboxVisual;
    [SerializeField] private bool logStateChanges = true;
    [SerializeField] private State currentState = State.Waiting;

    private Camera cam;
    private Vector2 aimDirection = Vector2.up;
    private Vector2 attackDirection = Vector2.up;
    private float stateTimer;
    private readonly HashSet<EnemyHealth> hitEnemies = new HashSet<EnemyHealth>();

    private void Awake()
    {
        cam = Camera.main;
        SetVisual(false);
    }
    private void Update()
    {
        AimAtMouse();
        stateTimer += Time.deltaTime;
        switch (currentState)
        {
            case State.Waiting:
                UpdateWaiting();
                break;

            case State.Attacking:
                UpdateAttacking();
                break;
        }
    }

    private void UpdateWaiting()
    {
        if (stateTimer >= attackInterval)
        {
            ChangeState(State.Attacking);
        }
    }
    private void UpdateAttacking()
    {
        DealDamageInHitbox();
        UpdateVisual();
        if (stateTimer >= hitboxDuration)
        {
            ChangeState(State.Waiting);
        }
    }
    private void ChangeState(State newState)
    {
        if (logStateChanges)
        {
            Debug.Log("Attack: " + currentState + " -> " + newState);
        }
        ExitState(currentState);
        currentState = newState;
        stateTimer = 0f;
        EnterState(newState);
    }

    private void EnterState(State state)
    {
        if (state == State.Attacking)
        {
            attackDirection = aimDirection;
            hitEnemies.Clear();
            UpdateVisual();
            SetVisual(true);
        }
    }

    private void ExitState(State state)
    {
        if (state == State.Attacking)
        {
            SetVisual(false);
        }
    }
    private void DealDamageInHitbox()
    {
        Vector2 size = new Vector2(hitboxWidth, hitboxLength);
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            GetHitboxCenter(), size, GetHitboxAngle(), enemyLayer);

        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            if (enemy == null) continue;
            if (!hitEnemies.Add(enemy)) continue;
            enemy.TakeDamage(damage);
        }
    }
    private Vector2 GetHitboxCenter()
    {
        return (Vector2)transform.position + attackDirection * hitboxDistance;
    }

    private float GetHitboxAngle()
    {
        return Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg - 90f;
    }
    private void AimAtMouse()
    {
        if (Mouse.current == null || cam == null) return;
        Vector2 mouseScreen = Mouse.current.position.ReadValue();  
        Vector2 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);   
        Vector2 direction = mouseWorld - (Vector2)transform.position;
        if (direction.sqrMagnitude > 0.0001f)
        {
            aimDirection = direction.normalized;   
        }
    }
    private void SetVisual(bool visible)
    {
        if (hitboxVisual != null)
        {
            hitboxVisual.SetActive(visible);
        }
    }
    private void UpdateVisual()
    {
        if (hitboxVisual == null) return;
        hitboxVisual.transform.position = GetHitboxCenter();
        hitboxVisual.transform.rotation = Quaternion.Euler(0f, 0f, GetHitboxAngle());
        hitboxVisual.transform.localScale = new Vector3(hitboxWidth, hitboxLength, 1f);
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 direction = Application.isPlaying ? aimDirection : Vector2.up;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Vector3 center = transform.position + (Vector3)(direction * hitboxDistance);
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.Euler(0f, 0f, angle), Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(hitboxWidth, hitboxLength, 0f));
        Gizmos.matrix = Matrix4x4.identity;
    }
}