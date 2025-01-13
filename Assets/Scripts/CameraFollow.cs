using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The boat's Transform
    public Vector3 offset; // Offset of the camera relative to the boat
    public float smoothSpeed = 0.125f; // Speed of the camera smoothing

    void LateUpdate()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;

        // Optional: Make the camera look at the boat
        transform.LookAt(target);
    }
}
