using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 60f; // tempo in secondi

    private float timeLeft;
    private UIController uiController;
    private LifeController lifeController;
    private bool isRunning = true;

    void Start()
    {
        timeLeft = startTime;
        uiController = FindObjectOfType<UIController>();
        lifeController = FindObjectOfType<LifeController>();
    }

    void Update()
    {
        if (!isRunning) return;

        timeLeft -= Time.deltaTime;
        timeLeft = Mathf.Max(timeLeft, 0);

        if (uiController != null)
            uiController.UpdateTimer(timeLeft);

        if (timeLeft <= 0 && lifeController != null)
        {
            isRunning = false;
            lifeController.Die();
        }
    }
}
