using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScript : MonoBehaviour
{
    public AudioClip WalkingSound, RunningSound;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isWalking)
        {
            if (isRunning)
            {
                // If currently not playing the running sound, switch to it
                if (audioSource.clip != RunningSound)
                {
                    audioSource.clip = RunningSound;
                    audioSource.Play();
                }
            }
            else
            {
                // If not running and currently not playing the walking sound, switch to it
                if (audioSource.clip != WalkingSound)
                {
                    audioSource.clip = WalkingSound;
                    audioSource.Play();
                }
            }
        }
        else
        {
            // If neither walking nor running, stop the sounds
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}
