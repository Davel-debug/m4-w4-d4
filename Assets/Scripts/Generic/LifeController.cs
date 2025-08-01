using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    [Header("Life Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    private UIController uiController;

    void Start()
    {
        currentHealth = maxHealth;
        uiController = FindObjectOfType<UIController>();
        if (uiController != null)
            uiController.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (uiController != null)
            uiController.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (uiController != null)
            uiController.UpdateHealth(currentHealth, maxHealth);
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} is dead.");
        SceneTracker.Instance.lastLevel = SceneManager.GetActiveScene().name;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        uiController?.UpdateHealth(currentHealth, maxHealth); // se hai il riferimento o lo passi
    }
}
