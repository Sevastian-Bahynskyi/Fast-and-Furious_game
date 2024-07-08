using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 10.0f; // Normal movement speed
    public float boostMultiplier = 5.0f; // Speed multiplier when boost is active
    public float sensitivity = 100.0f; // Mouse sensitivity

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Update()
    {
        // Get input from arrow keys or WASD keys for horizontal and vertical movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get input from Q and E keys for up and down movement
        float up = 0.0f;
        if (Input.GetKey(KeyCode.Q)) {
            up = -1.0f;
        } else if (Input.GetKey(KeyCode.E)) {
            up = 1.0f;
        }

        // Calculate the direction based on the input
        Vector3 direction = new Vector3(horizontal, up, vertical);
        direction = transform.TransformDirection(direction);
        
        // Check if the boost key is held down
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.F)) {
            currentSpeed *= boostMultiplier;
        }

        // Move the camera
        transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);

        // Rotate the camera based on mouse input
        rotationX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f); // Limit the vertical rotation

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0.0f);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}