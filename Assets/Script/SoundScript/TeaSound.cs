using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaSound : MonoBehaviour
{
    public static TeaSound Instance { get; private set; }
    public AudioClip drinkTeaSound; // Assign this through the Inspector
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource component not found on the Player!");
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayTeaDrinkingSound()
    {
        if (drinkTeaSound == null)
        {
            Debug.LogError("DrinkTeaSound AudioClip is not assigned on TeaSound script!");
            return;
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not set or found on TeaSound script!");
            return;
        }

        audioSource.PlayOneShot(drinkTeaSound);
    }
}
