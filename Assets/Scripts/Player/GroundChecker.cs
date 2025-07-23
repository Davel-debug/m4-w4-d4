using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float coyoteTimeDuration = 0.2f;
    private float coyoteStartTime;
    private bool isInCoyoteTime;
    public bool IsInCoyoteTime => isInCoyoteTime;

    private bool isGrounded;
    public bool IsGrounded => isGrounded;

    public UnityEvent<bool> onGroundStateChanged;

    private bool jumpTriggered;
    private byte jumpFramesRemaining;

    public void TriggerJump()
    {
        jumpTriggered = true;
    }

    void Start()
    {
        onGroundStateChanged?.Invoke(isGrounded);
    }

    void Update()
    {
        if (jumpTriggered)
        {
            jumpTriggered = false;
            jumpFramesRemaining = 5;
            coyoteStartTime = 0;
            isInCoyoteTime = false;
        }
        else if (jumpFramesRemaining > 0)
        {
            jumpFramesRemaining--;
            // NON impostare isGrounded a false qui
        }
        else
        {
            bool sphereCheck = CheckGroundSphere();

            if (sphereCheck)
            {
                if (!isGrounded)
                {
                    isGrounded = true;
                    onGroundStateChanged?.Invoke(isGrounded);
                }

                isInCoyoteTime = false;
                coyoteStartTime = 0;
            }
            else
            {
                if (isGrounded)
                {
                    isGrounded = false;
                    onGroundStateChanged?.Invoke(isGrounded);
                    isInCoyoteTime = true;
                    coyoteStartTime = Time.time;
                }
                else if (isInCoyoteTime && (Time.time - coyoteStartTime > coyoteTimeDuration))
                {
                    isInCoyoteTime = false;
                    coyoteStartTime = 0;
                }
            }
        }
    }
    private bool CheckGroundSphere()
    {
        return Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
