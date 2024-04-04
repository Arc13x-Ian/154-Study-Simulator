using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    public Transform player; // Reference to the player object

    void Update()
    {
        // Ensure player is not null
        if (player != null)
        {
            // Calculate direction vector from sprite to player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f; // Ensure the sprite doesn't tilt up or down

            // Rotate sprite to face the player
            if (directionToPlayer != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(directionToPlayer);
            }
        }
        else
        {
            Debug.LogWarning("Player object not assigned for facing.");
        }
    }
}
