using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform targetTransform; // Player's transform
    public Transform spawnPointTransform; // Current spawn point's transform
    public float shakeDuration = 0.5f;
    public float shakeIntensity = 0.3f;

    private Vector3 initialPosition;
    private float shakeTimer = 0f;

    private void Awake()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        initialPosition = cameraTransform.localPosition;
    }

    private void Update()
    {
        initialPosition = cameraTransform.localPosition;

        if (shakeTimer > 0f)
        {
            // Generate a random offset within a range
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;

            // Apply the shake offset to the camera's position
            cameraTransform.localPosition = initialPosition + shakeOffset;

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset the camera's position relative to the spawn point
            cameraTransform.position = spawnPointTransform.position + initialPosition;
        }
    }

    public void Shake()
    {
        shakeTimer = shakeDuration;
    }
}
