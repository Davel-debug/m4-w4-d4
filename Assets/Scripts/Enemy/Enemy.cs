using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attacco")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float projectileSpeed = 20f;

    [Header("Rilevamento")]
    public float detectionRange = 15f;
    public LayerMask playerLayer;

    private Transform player;
    private float fireCooldown = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > detectionRange) return;

        RotateTowardsPlayer();

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
        else
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projectile = proj.GetComponent<Projectile>();
        if (projectile != null)
        {
            Vector3 direction = (player.position - firePoint.position).normalized;
            projectile.LaunchStraight(direction * projectileSpeed);
        }
    }
}
