using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    [SerializeField] private float healPerSecond = 10f;
    [SerializeField] private float pollutionPerHealth = 0.5f;
    [SerializeField] private float lifetime = 10f;

    private Transform player;
    private PlayerHealth playerHealth;
    private Emotion Emotion;

    private void Awake()
    {
        ApplyRadius();

        if (lifetime > 0f)
        {
            Destroy(gameObject, lifetime);
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
            Emotion = playerObject.GetComponent<Emotion>();

        }
    }

    private void OnValidate()
    {
        ApplyRadius();
    }

    private void Update()
    {
        if (player == null || playerHealth == null) return;
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > radius) return;
        float healed = playerHealth.Heal(healPerSecond * Time.deltaTime);
        if (Emotion != null)
        {
            Emotion.AddPollution(healed * pollutionPerHealth);
        }
    }
    private void ApplyRadius()
    {
        transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
    }
}