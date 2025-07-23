using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>();
        if (life != null)
        {
            life.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
