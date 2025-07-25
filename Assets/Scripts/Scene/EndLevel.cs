using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class EndLevel : MonoBehaviour
{
    [Header("Requisiti")]
    [SerializeField] private int requiredPoints = 10;
    [SerializeField] private string victorySceneName = "VictoryScreen";

    [Header("Feedback")]
    [SerializeField] private GameObject messagePanel; // Pannello messaggio UI
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float messageDuration = 3f;

    private Coroutine hideMessageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        int currentPoints = inventory != null ? inventory.GetTotalPoints() : 0;

        if (currentPoints >= requiredPoints)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene(victorySceneName);
        }
        else
        {
            int missing = requiredPoints - currentPoints;
            ShowMessage($"Eh volevi, stupida scipola che non sei altro\nTi servono altri {missing} punti per completare il livello!");
        }
    }

    private void ShowMessage(string text)
    {
        if (messageText != null) messageText.text = text;
        if (messagePanel != null) messagePanel.SetActive(true);

        if (hideMessageCoroutine != null)
            StopCoroutine(hideMessageCoroutine);

        hideMessageCoroutine = StartCoroutine(HideMessageAfterDelay());
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        if (messagePanel != null) messagePanel.SetActive(false);
    }
}
