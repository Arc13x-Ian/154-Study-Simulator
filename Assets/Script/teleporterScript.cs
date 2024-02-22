using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporterScript : MonoBehaviour
{
    public GameObject[] teleportPoints = null;
    public int iterationCount = 10;
    public Rigidbody Snackerman;

    // Start is called before the first frame update
    void Start()
    {
        Snackerman = GetComponent<Rigidbody>();
        teleportPoints = GameObject.FindGameObjectsWithTag("Study Spot");
        Invoke("MovementManagement", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MovementManagement()
    {
        for(int i = 0; i < iterationCount; i++)
        {
            Invoke("Teleport", 2.0f);
        }
    }

    void Teleport()
    {
        int index = 0;
        Snackerman.MovePosition(teleportPoints[index].transform.position);
        index++;
        Debug.Log("I have attempted to teleport!");


    }
}
