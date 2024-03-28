using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    public AudioClip soundEffect;
    public float delayInSeconds = 1.0f;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        audioSource.clip = soundEffect;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(PlaySoundAfterDelay());
        }
    }

    private IEnumerator PlaySoundAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
