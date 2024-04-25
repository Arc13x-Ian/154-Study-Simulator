using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearBeatScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip heartbeat1;
    public AudioClip heartbeat2;
    public AudioClip heartbeat3;
    private AnxityMeter anxityMeter;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HeartbeatSoundManager script started.");
        audioSource = GetComponent<AudioSource>();
        GameObject player = GameObject.FindGameObjectWithTag("UI");
        if (player != null)
        {
            anxityMeter = player.GetComponent<AnxityMeter>();
            if (anxityMeter == null)
            {
                Debug.LogError("AnxityMeter component not found on the Player object.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaySound(heartbeat1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaySound(heartbeat2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlaySound(heartbeat3);
        }

    }


    void UpdateHeartBeatSound(float anxietyLevel)
    {
        if (anxietyLevel <= 3 && audioSource.clip != heartbeat1)
        {
            audioSource.clip = heartbeat1;
            audioSource.Play();
        }
        else if (anxietyLevel > 3 && anxietyLevel <= 6 && audioSource.clip != heartbeat2)
        {
            audioSource.clip = heartbeat2;
            audioSource.Play();
        }
        else if (anxietyLevel > 6 && anxietyLevel <= 9 && audioSource.clip != heartbeat3)
        {
            audioSource.clip = heartbeat3;
            audioSource.Play();
        }


    }
    void PlaySound(AudioClip clip)
    {
        if (audioSource.clip != clip || !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
