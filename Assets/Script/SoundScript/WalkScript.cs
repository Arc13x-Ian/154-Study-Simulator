using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScript : MonoBehaviour
{
    public AudioClip WalkingSound, RunningSound, JumpSound;
    private AudioSource audioSource;
    private bool wasJumping = false;

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
        bool isJumping = Input.GetKey(KeyCode.Space);

        if (isJumping && !wasJumping)
        {
            PlayJumpSound();
            wasJumping = true; // Avoid playing the sound repeatedly while space is held down
        }
        else if (!isJumping)
        {
            wasJumping = false; // Reset when space is not being pressed
            WalkingRunningSounds(isWalking, isRunning);
        }


    }
    private void WalkingRunningSounds(bool isWalking, bool isRunning)
    {
        if (isWalking)
        {
            if (isRunning)
            {
                // If currently not playing the running sound, switch to it
                if (audioSource.clip != RunningSound || !audioSource.isPlaying)
                {
                    audioSource.loop = true;
                    audioSource.clip = RunningSound;
                    audioSource.Play();
                }
            }
            else
            {
                // If not running and currently not playing the walking sound, switch to it
                if (audioSource.clip != WalkingSound || !audioSource.isPlaying)
                {
                    audioSource.loop = true;
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

    private void PlayJumpSound()
    {
        
        audioSource.loop = false;
        audioSource.clip = JumpSound;
        audioSource.Play();
       
    }

}
