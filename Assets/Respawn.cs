using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public Transform respawnSpot;

    void OnTriggerEnter(Collider other)
    {
        RespawnPlayer(player);
    }


    private void RespawnPlayer(GameObject player)
    {

            // Teleport the player to the respawn spot
            player.transform.position = new Vector3(respawnSpot.position.x, respawnSpot.position.y, respawnSpot.position.z);
            Debug.Log("Player respawned at x:" + respawnSpot.position.x + " y:" + respawnSpot.position.y + " z" + respawnSpot.position.z);
        
    }
}
