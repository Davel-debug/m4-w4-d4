using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void LaunchStraight(Vector3 velocity)
    {
        rb.useGravity = false;
        rb.velocity = velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>();
        if (life != null)
        {
            life.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
