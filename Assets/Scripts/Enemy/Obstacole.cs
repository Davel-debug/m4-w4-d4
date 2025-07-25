using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObstacleType
    {
        MoveHorizontal,
        MoveVertical,
        RotateFromPoint,
        RotateSelf,
        DamageOverTime,
        InstantKill
    }

    public ObstacleType type;

    [Header("Movimento")]
    public float distance = 2f;
    public float speed = 1f;

    [Header("Rotazione")]
    public Vector3 rotationAxis = Vector3.up;

    [Header("Danno")]
    public float damagePerSecond = 10f;

    private Vector3 startPos;
    private float dir = 1f;
    private LifeController playerLife;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        switch (type)
        {
            case ObstacleType.MoveHorizontal:
                Move(Vector3.right);
                break;

            case ObstacleType.MoveVertical:
                Move(Vector3.up);
                break;

            case ObstacleType.RotateFromPoint:
                RotateAndMove(Vector3.forward);
                break;

            case ObstacleType.RotateSelf:
                RotateInPlace();
                break;
        }
    }

    void Move(Vector3 direction)
    {
        Vector3 targetA = startPos;
        Vector3 targetB = startPos + direction.normalized * distance;

        transform.position = Vector3.MoveTowards(transform.position, dir > 0 ? targetB : targetA, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, dir > 0 ? targetB : targetA) < 0.01f)
            dir *= -1f;
    }



    void RotateAndMove(Vector3 direction)
    {
        transform.Rotate(rotationAxis.normalized * speed * Time.deltaTime);
        transform.Translate(direction * speed * dir * Time.deltaTime, Space.World);
        float moved = Vector3.Dot(transform.position - startPos, direction);

        if (moved >= distance)
            dir = -1f;
        else if (moved <= 0f)
            dir = 1f;
    }

    void RotateInPlace()
    {
        transform.Rotate(rotationAxis.normalized * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerLife ??= other.GetComponent<LifeController>();
        if (playerLife == null) return;

        if (type == ObstacleType.DamageOverTime)
            playerLife.TakeDamage(damagePerSecond * Time.deltaTime);
        else if (type == ObstacleType.InstantKill)
            playerLife.Die();
    }
}
