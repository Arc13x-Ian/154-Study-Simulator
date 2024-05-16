using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    public float hoverHeight = 0.5f; // Adjust this value to change the hover height
    public float hoverSpeed = 1.0f; // Adjust this value to change the hover speed

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave to create a hover effect
        float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
