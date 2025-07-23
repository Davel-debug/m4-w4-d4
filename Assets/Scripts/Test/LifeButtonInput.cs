using UnityEngine;
using UnityEngine.UI;

public class LifeButtonInput : MonoBehaviour
{
    public Button damageButton;
    public Button healButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // ad esempio: K per danneggiare
            damageButton.onClick.Invoke();

        if (Input.GetKeyDown(KeyCode.H)) // ad esempio: H per curare
            healButton.onClick.Invoke();
    }
}
