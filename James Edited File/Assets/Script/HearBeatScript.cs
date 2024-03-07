using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearBeatScript : MonoBehaviour
{
    private AudioSource heartbeatAudioSource;
    public AudioClip heartbeatSound;
    // Start is called before the first frame update
    void Start()
    {
        heartbeatAudioSource = GetComponent<AudioSource>();
        if (heartbeatAudioSource == null)
        {
            heartbeatAudioSource = gameObject.AddComponent<AudioSource>();
        }
        heartbeatAudioSource.clip = heartbeatSound;
        heartbeatAudioSource.loop = true;
        heartbeatAudioSource.playOnAwake = false;

        PlayHeartbeat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayHeartbeat()
    {
        if (!heartbeatAudioSource.isPlaying)
        {
            heartbeatAudioSource.Play();
        }
    }

    public void StopHeartbeat()
    {
        if (heartbeatAudioSource.isPlaying)
        {
            heartbeatAudioSource.Stop();
        }
    }
}
