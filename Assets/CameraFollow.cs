using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform.
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Adjust the offset as needed.

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the camera's new position based on the target's position and offset.
            Vector3 newPosition = target.position + offset;
            transform.position = newPosition;

            // Make the camera look at the target.
            transform.LookAt(target);
        }
    }
}
