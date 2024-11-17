using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The target to follow (e.g., the player)
    public float distance = 5.0f; // Distance from the target
    public float height = 2.0f; // Height offset from the target
    public float rotationSpeed = 100.0f; // Speed of camera rotation
    public float smoothSpeed = 0.1f; // Smoothing speed for camera movement

    private float currentRotationY = 0f; // Current horizontal rotation
    private float currentRotationX = 0f; // Current vertical rotation
    private Vector3 currentVelocity = Vector3.zero;

    void LateUpdate()
    {
        if (!target) return; // Exit if there's no target

        // Handle mouse input
        float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float verticalInput = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Update rotation values
        currentRotationY += horizontalInput;
        currentRotationX = Mathf.Clamp(currentRotationX + verticalInput, -30f, 60f); // Clamp vertical rotation

        // Calculate the desired position
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        Vector3 targetPosition = target.position - (rotation * Vector3.forward * distance) + Vector3.up * height;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothSpeed);

        // Make the camera look at the target
        transform.LookAt(target.position + Vector3.up * height);
    }
}
