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
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayTeaDrinkingSound()
    {
        if (drinkTeaSound != null)
        {
            audioSource.PlayOneShot(drinkTeaSound);
        }
    }
}
