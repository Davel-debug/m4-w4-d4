using UnityEngine;
using UnityEngine.UI;

public class LifeButtonInput : MonoBehaviour
{
    private Button damageButton;
    private Button healButton;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (damageButton == null)
            damageButton = GameObject.Find("DamageButton")?.GetComponent<Button>();

        if (healButton == null)
            healButton = GameObject.Find("HealButton")?.GetComponent<Button>();

        if (damageButton != null && Input.GetKeyDown(KeyCode.K))
            damageButton.onClick.Invoke();

        if (healButton != null && Input.GetKeyDown(KeyCode.H))
            healButton.onClick.Invoke();
    }
}
