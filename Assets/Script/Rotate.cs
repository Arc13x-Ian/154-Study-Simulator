using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform centerObject; // The object to rotate around
    public float rotationSpeedAroundCenter = 30f; // Speed of rotation around the center object
    public float rotationSpeedLocal = 60f; // Speed of local rotation

    void Update()
    {
        // Ensure centerObject is not null
        if (centerObject != null)
        {
            // Rotate around the centerObject's position
            transform.RotateAround(centerObject.position, Vector3.up, rotationSpeedAroundCenter * Time.deltaTime);

            // Rotate locally
            transform.Rotate(Vector3.up, rotationSpeedLocal * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Center object not assigned for rotation.");
        }
    }
}
