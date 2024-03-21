using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{


    public AudioClip PickupSound;
    private AudioSource audioSource;


    //for the right hand
    public void BookInteract(bool EmptyHand)
    {
        // Implement book interaction logic here
        Debug.Log("Interacting with " + gameObject.name);

        if(gameObject.tag == "Book" && EmptyHand)
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

        if (gameObject.tag == "headPhones")
        {
            Destroy(gameObject);
            audioSource.PlayOneShot(PickupSound);
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
