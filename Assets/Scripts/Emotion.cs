using UnityEngine;

public class Emotion : MonoBehaviour
{
    [SerializeField] private float maxEmotion = 100f;

    private float currentEmotion;
    private PlayerHealth playerHealth;

    public float CurrentEmotion => currentEmotion;
    public float MaxEmotion => maxEmotion;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void AddPollution(float amount)
    {
        currentEmotion = Mathf.Min(currentEmotion + amount, maxEmotion);

        if (currentEmotion >= maxEmotion)
        {
            Debug.Log("Distorted");
            playerHealth.Die();
        }
    }
}