using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastSphere : MonoBehaviour
{
    public Transform targetTransform;
    public RectTransform uiElement;
    public float moveSpeed = 5f;
    public float minDistanceToCenter = 100f;
    public float maxX = 500f; // Maximum allowed x position
    public float maxY = 300f; // Maximum allowed y position

    void Update()
    {
        if (targetTransform == null || uiElement == null)
        {
            Debug.LogWarning("Target transform or UI element not assigned.");
            return;
        }

        // Calculate direction towards the target transform
        Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

        // Calculate distance to the target transform
        float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

        // Calculate direction towards the center of the screen
        Vector3 directionToCenter = (Vector3.zero - transform.position).normalized;

        // Move the UI element towards the center of the screen based on distance to the target
        float moveDistance = Mathf.Lerp(0f, minDistanceToCenter, Mathf.Clamp01(distanceToTarget / minDistanceToCenter));
        Vector3 newPosition = transform.position + directionToCenter * moveDistance * Time.deltaTime;

        // Move the UI element towards the target transform
        newPosition += directionToTarget * moveSpeed * Time.deltaTime;

        // Clamp the UI element's position within the specified range
        newPosition.x = Mathf.Clamp(newPosition.x, -maxX + Screen.width / 2f, maxX - Screen.width / 2f);
        newPosition.y = Mathf.Clamp(newPosition.y, -maxY + Screen.height / 2f, maxY - Screen.height / 2f);

        // Update the UI element's position
        uiElement.position = newPosition;
    }
}