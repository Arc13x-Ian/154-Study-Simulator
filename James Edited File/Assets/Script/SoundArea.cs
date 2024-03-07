using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundArea : MonoBehaviour
{
    private AudioSource audioSource;
    public float soundRange = 20f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if( PlayerIsInRange())
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        
    }

    bool PlayerIsInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        return distanceToPlayer <= soundRange;
    }
}
