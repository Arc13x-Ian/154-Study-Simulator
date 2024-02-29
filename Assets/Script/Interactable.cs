using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private GameObject StoredItem;


    //for the right hand
    public void BookInteract(bool EmptyHand)
    {
        // Implement book interaction logic here
        Debug.Log("Interacting with " + gameObject.name);

        if(gameObject.tag == "Book" && EmptyHand)
        {
            StoredItem = gameObject;
            Destroy(gameObject);
            
            
        }
    }

    public void putBookDown(bool EmptyHand)
    {
        Debug.Log("Interacting with " + gameObject.name);

        if (gameObject.tag == "Study Spot" && EmptyHand == false)
        {
            //set something up to put the book down on




        }
    }

    //for the Left hand
    public void ItemInteract(bool EmptyHand)
    {
        if (gameObject.tag == "Tea" && EmptyHand == true)
        {
            Destroy(gameObject);
        }

        if (gameObject.tag == "Ear Plugs")
        {

        }




    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
