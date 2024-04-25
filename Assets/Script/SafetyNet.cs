using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SafetyNet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("SafetyNet"))
        {
            transform.position = transform.position + new Vector3(0.0f, 0.0f, 1000f);

            SceneManager.LoadScene(2);
        }

    }
}
