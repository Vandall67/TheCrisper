using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Orbit Settings")]
    public float distance = 5f;
    public float mouseSensitivity = 3f;

    [Header("Vertical Clamp")]
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 60f;

    [Header("Smoothing")]
    public float smoothSpeed = 10f;

    [Header("CameraPosition")]
    public float xCameraDeviation = 0.5f;
    public float yCameraDeviation = 0.2f;
    public float deviationResetSpeed = 5f;   // how fast it centers when moving
    public float deviationReturnSpeed = 3f;

    [Header("MainBody")] 
    [SerializeField] private GameObject Mainbody;

    private float yaw;    // horizontal rotation
    private float pitch;  // vertical rotation
    private Vector3 deviationOffset;
    private Vector3 targetDeviation;

    void Start()
    {
        // Initialize angles from current camera orientation
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;

        // Build the offset once, never touched again
        deviationOffset = new Vector3(xCameraDeviation, yCameraDeviation, 0f);
        targetDeviation = deviationOffset; 

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Read mouse input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

        bool isMoving = Input.GetKey(KeyCode.W);
        if (!Mainbody.activeSelf)
        {
            isMoving = false;
        }

        if (isMoving)
            targetDeviation = Vector3.zero;
        else
            targetDeviation = new Vector3(xCameraDeviation, yCameraDeviation, 0f);

        float speed = isMoving ? deviationResetSpeed : deviationReturnSpeed;
        deviationOffset = Vector3.Lerp(deviationOffset, targetDeviation, speed * Time.deltaTime);




        // Calculate desired rotation and position
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);


        Vector3 desiredPosition = target.position - rotation * Vector3.forward * distance + rotation * deviationOffset;

        // Smooth and apply
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothSpeed * Time.deltaTime);
    }
}