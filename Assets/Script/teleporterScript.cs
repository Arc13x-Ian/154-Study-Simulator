using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporterScript : MonoBehaviour
{
    public GameObject[] teleportPoints = null;
    public int iterationCount = 3;
    public Rigidbody Snackerman;
    BoxCollider StudyCollider;


    // Start is called before the first frame update
    void Start()
    {
        Snackerman = GetComponent<Rigidbody>();
        teleportPoints = GameObject.FindGameObjectsWithTag("Study Spot");
        StartCoroutine(Portloop());
    }

    // Update is called once per frame
    void Update()
    {
        for(int x = 0; x < teleportPoints.Length; x++)
        {
            if(gameObject.transform.position == teleportPoints[x].transform.position)
            {
                teleportPoints[x].SetActive(false);

            }
            else
            {
                teleportPoints[x].SetActive(true);
            }
        }
        
    }

    void MovementManagement()
    {
       
    }

    void Teleport()
    {
       


    }

    IEnumerator Portloop()
    {
        int index = 0;

        for (int i = 0; i < iterationCount; i++)
        {
            
            Snackerman.MovePosition(teleportPoints[index].transform.position);
            index++;
            Debug.Log("I have attempted to teleport!");

            yield return new WaitForSeconds(5);
        }

        Snackerman.MovePosition(teleportPoints[1].transform.position);

        StartCoroutine(Replay());

    }

    IEnumerator Replay()
    {
        yield return new WaitForSeconds(5);

        StartCoroutine(Portloop());
    }
}
