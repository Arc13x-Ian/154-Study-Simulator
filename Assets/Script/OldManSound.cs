using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManSound : MonoBehaviour
{
    public AudioClip soundToPlay;
    private AudioSource audioSource;
    public float probability = 0.5f; // 50% chance 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = soundToPlay;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !audioSource.isPlaying)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // Random chance to play the sound
                if (Random.value < probability)
                {
                    audioSource.Play();
                }
            }
        }
    }
}
