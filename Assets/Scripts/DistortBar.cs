using UnityEngine;
using UnityEngine.UI;

public class DistortBar : MonoBehaviour
{
    [SerializeField] private Emotion Emotion;
    [SerializeField] private Image fillImage;

    private void Update()
    {
        if (Emotion == null) return;

        fillImage.fillAmount = Emotion.CurrentEmotion / Emotion.MaxEmotion;
    }
}