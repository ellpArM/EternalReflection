using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
    public Transform target;       // The target the camera will follow (e.g., the player)
    public float smoothSpeed = 0.125f;  // The speed of the smooth movement (lower is smoother but slower)
    public Vector3 offset;         // Offset from the target to position the camera
    public bool useSmoothDamp = false; // Toggle between Lerp and SmoothDamp

    private Vector3 velocity = Vector3.zero; // Used for SmoothDamp

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for SmoothCameraFollow.");
            return;
        }

        // Desired position of the camera
        Vector3 desiredPosition = target.position + offset;

        if (useSmoothDamp)
        {
            // Smoothly move the camera using SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }
        else
        {
            // Smoothly move the camera using Lerp
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
