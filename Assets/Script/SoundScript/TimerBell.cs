using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBell : MonoBehaviour
{
    private AudioSource audioSource;
    private float timerDuration = 454.0f; 
    private float timeElapsed = 0.0f;
    private bool hasPlayed = false; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!hasPlayed) 
        {
            timeElapsed += Time.deltaTime; 

            if (timeElapsed >= timerDuration)
            {
                if (audioSource && !audioSource.isPlaying)
                {
                    audioSource.Play(); 
                    hasPlayed = true; 
                }
            }
        }
    }
}
