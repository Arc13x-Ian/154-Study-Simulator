using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIcon : MonoBehaviour
{
    public GameObject targetObject; // The object around which the sprite will rotate
    public GameObject spriteObject; // The sprite object to rotate
    public float rotationSpeed = 50f; // Rotation speed

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        if (targetObject == null || spriteObject == null || mainCamera == null)
        {
            Debug.LogError("Target object, sprite object, or main camera is not assigned!");
            enabled = false; // Disable the script if required components are not assigned
            return;
        }

        spriteObject.SetActive(false); // Initially hide the sprite
    }

    private void Update()
    {
        // Cast a ray from the camera to detect if the player is looking at the target object
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        {
            if (hit.collider.gameObject == targetObject)
            {
                // Player is looking at the target object, show and rotate the sprite
                spriteObject.SetActive(true);
                RotateSprite();
            }
            else
            {
                // Player is not looking at the target object, hide the sprite
                spriteObject.SetActive(false);
            }
        }
    }

    private void RotateSprite()
    {
        // Rotate the sprite around the target object's Y-axis
        spriteObject.transform.RotateAround(targetObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
