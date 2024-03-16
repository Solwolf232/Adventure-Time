using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Playertarget; // The player's transform
    public float smoothSpeed = 0.125f; // The smoothing factor
    public Vector3 offset; // The offset from the player

    private void FixedUpdate()
    {
        if (Playertarget == null)
            return;


        // Calculate the desired position for the camera
        Vector3 desiredPosition = Playertarget.position + offset;

        // Use Lerp to smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;

    }
}
