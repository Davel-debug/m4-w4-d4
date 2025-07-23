using UnityEngine;
using UnityEngine.UI;
using TMPro; // se usi TextMeshPro

public class UIController : MonoBehaviour
{
    [Header("Health UI")]
    public Slider healthBar;
    public TextMeshProUGUI healthText;

    [Header("Score UI")]
    public TextMeshProUGUI scoreText;

    [Header("Timer UI")]
    public TextMeshProUGUI timerText;

    [Header("PowerUp UI")]
    public Image powerUpIcon;
    public TextMeshProUGUI powerUpCountText;

    private void Start()
    {
        powerUpIcon.gameObject.SetActive(false);
        powerUpCountText.gameObject.SetActive(false);
    }

    public void UpdateHealth(float current, float max)
    {
        healthBar.value = current / max;
        healthText.text = $"{Mathf.CeilToInt(current)} / {Mathf.CeilToInt(max)}";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void UpdateTimer(float timeLeft)
    {
        timerText.text = $"Time: {timeLeft:F1}s";
    }

    public void ShowPowerUp(Sprite icon, int count)
    {
        powerUpIcon.sprite = icon;
        powerUpIcon.gameObject.SetActive(true);

        powerUpCountText.text = $"x{count}";
        powerUpCountText.gameObject.SetActive(true);
    }

    public void HidePowerUp()
    {
        powerUpIcon.gameObject.SetActive(false);
        powerUpCountText.gameObject.SetActive(false);
    }
}
