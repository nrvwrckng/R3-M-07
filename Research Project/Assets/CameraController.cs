using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float smoothSpeed = 1f; // Smoothness of camera movement
    public float lookAheadDistance = 4f; // Distance the camera looks ahead in the player's movement direction

    private Vector3 offset; // Offset between camera and player 
    private Vector3 desiredPosition; // Desired position of the camera
    private Vector3 currentVelocity; // Current velocity for smooth damp

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // Calculate the desired position of the camera
        desiredPosition = target.position + offset;

        // Check if the player is moving to the left and facing left
        if (target.GetComponent<Movemint>().IsFacingLeft && target.GetComponent<Rigidbody2D>().velocity.x < 0f)
        {
            // Lock the camera to the player's position
            desiredPosition.x = target.position.x;
        }
        else if (!target.GetComponent<Movemint>().IsFacingLeft && target.GetComponent<Rigidbody2D>().velocity.x > 0f)
        {
            // Calculate the target lookahead position based on player's movement direction
            float targetLookAhead = lookAheadDistance * Mathf.Sign(target.GetComponent<Rigidbody2D>().velocity.x);
            desiredPosition.x += targetLookAhead;
        }

        // Smoothly move the camera towards the desired position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
    }
}
