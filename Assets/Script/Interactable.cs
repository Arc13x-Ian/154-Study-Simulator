using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private GameObject StoredItem;
    public AudioClip PickupSound;
    private AudioSource audioSource;


    //for the right hand
    public void BookInteract(bool EmptyHand)
    {
        // Implement book interaction logic here
        Debug.Log("Interacting with " + gameObject.name);

        if(gameObject.tag == "Book" && EmptyHand)
        {
            StoredItem = gameObject;
            if (audioSource != null && PickupSound != null)
            {
                audioSource.PlayOneShot(PickupSound);
                Destroy(gameObject, PickupSound.length);
            }
            else
            {
                Destroy(gameObject);
            }
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
            if (audioSource != null && PickupSound != null)
            {
                audioSource.PlayOneShot(PickupSound);
                Destroy(gameObject, PickupSound.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (gameObject.tag == "Ear Plugs")
        {

        }




    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
