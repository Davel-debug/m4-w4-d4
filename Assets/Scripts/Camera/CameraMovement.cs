using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float height = 2f;
    public float sensitivityX = 2f;
    public float sensitivityY = 1.5f;
    public float yMin = -20f;
    public float yMax = 60f;

    private float rotX = 0f;
    private float rotY = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotX = angles.y;
        rotY = angles.x;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        rotX += Input.GetAxis("Mouse X") * sensitivityX;
        rotY -= Input.GetAxis("Mouse Y") * sensitivityY;
        rotY = Mathf.Clamp(rotY, yMin, yMax);

        Quaternion rotation = Quaternion.Euler(rotY, rotX, 0f);
        Vector3 position = rotation * new Vector3(0f, 0f, -distance) + target.position + Vector3.up * height;

        transform.rotation = rotation;
        transform.position = position;
    }

    public Vector3 GetCameraForward()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    public Quaternion GetCameraFlatRotation()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        if (forward == Vector3.zero)
            forward = Vector3.forward;
        return Quaternion.LookRotation(forward);
    }
}
