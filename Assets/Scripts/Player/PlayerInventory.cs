using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int score = 0;

    private LifeController lifeController;

    void Awake()
    {
        lifeController = GetComponent<LifeController>();
    }

    public void CollectPickup(Pickup.PickupType type, int value)
    {
        switch (type)
        {
            case Pickup.PickupType.Gold:
            case Pickup.PickupType.Silver:
            case Pickup.PickupType.Bronze:
                score += value;
                break;

            case Pickup.PickupType.Health:
                if (lifeController != null)
                {
                    lifeController.Heal(value);
                }
                break;

            case Pickup.PickupType.MaxHealth:
                if (lifeController != null)
                {
                    lifeController.maxHealth += value;
                    lifeController.currentHealth += value; // opzionale, se vuoi anche curarlo
                    lifeController.currentHealth = Mathf.Clamp(lifeController.currentHealth, 0, lifeController.maxHealth);
                }
                break;
        }

        Debug.Log($"Collected {type}, score: {score}, HP: {lifeController?.currentHealth}/{lifeController?.maxHealth}");
    }
}
