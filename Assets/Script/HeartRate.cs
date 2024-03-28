using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRate : MonoBehaviour
{
    private AudioSource heartbeatAudioSource;
    public AudioClip heartbeatSound;
    AnxityMeter AnxityMeter;


    // Start is called before the first frame update
    void Start()
    {
        AnxityMeter = FindAnyObjectByType<AnxityMeter>();

        heartbeatAudioSource = gameObject.AddComponent<AudioSource>();

        heartbeatAudioSource.clip = heartbeatSound;
        heartbeatAudioSource.loop = true;
        heartbeatAudioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayHeartbeat();
    }
    public void PlayHeartbeat()
    {
        if (AnxityMeter.Anxietyscore < 30)
        {
            heartbeatAudioSource.pitch = 1;
        }

        if (AnxityMeter.Anxietyscore > 30 && AnxityMeter.Anxietyscore < 60)
        {
            heartbeatAudioSource.pitch = 1.2f;
        }

        if (AnxityMeter.Anxietyscore > 60)
        {
            heartbeatAudioSource.pitch = 1.5f;
        }


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
