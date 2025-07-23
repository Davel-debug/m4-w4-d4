using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundChecker))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private int maxJumpCount = 2;

    private Rigidbody _rb;
    private GroundChecker _groundCheck;

    private float _horizontal;
    private float _vertical;
    private bool _jumpRequest;
    private int _jumpCount;
    private bool wasGrounded;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _groundCheck = GetComponent<GroundChecker>();
        _rb.freezeRotation = true;
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        // Reset conteggio salti solo quando atterra (transizione da aria a terra)
        if (_groundCheck.IsGrounded && !wasGrounded)
        {
            _jumpCount = 0;
            Debug.Log("A terra - reset salti");
        }

        wasGrounded = _groundCheck.IsGrounded;

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < maxJumpCount)
        {
            _jumpRequest = true;
            _jumpCount++;
            Debug.Log("Salto #" + _jumpCount);
            _groundCheck.TriggerJump();
        }
    }

    void FixedUpdate()
    {
        Move();

        if (_jumpRequest)
        {
            _jumpRequest = false;

            // Annulla la velocità verticale prima del salto
            Vector3 velocity = _rb.velocity;
            velocity.y = 0f;
            _rb.velocity = velocity;

            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        Vector3 inputDir = new Vector3(_horizontal, 0f, _vertical).normalized;
        if (inputDir.sqrMagnitude > 0.01f)
        {
            Quaternion camRot = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            Vector3 moveDir = camRot * inputDir;

            Vector3 velocity = moveDir * moveSpeed;
            Vector3 velocityChange = velocity - new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(velocityChange, ForceMode.VelocityChange);

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
