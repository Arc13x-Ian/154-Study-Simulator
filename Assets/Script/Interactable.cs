using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    GameObject StoredItem;

    public void BookInteract(bool EmptyHand)
    {
        // Implement book interaction logic here
        Debug.Log("Interacting with " + gameObject.name);

        if(gameObject.tag == "Book" && EmptyHand)
        {
            
            Destroy(gameObject);
            
        }

        if (gameObject.tag == "Study Spot" && !EmptyHand)
        {

            

        }


    }

    public void ItemInteract(bool EmptyHand)
    {
        if (gameObject.tag == "Energy Drink")
        {

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
