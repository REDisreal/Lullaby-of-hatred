using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image fillImage;

    private void Update()
    {
        if (playerHealth == null)
        {
            fillImage.fillAmount = 0f;
            return;
        }

        fillImage.fillAmount = playerHealth.CurrentHealth / playerHealth.MaxHealth;
    }
}