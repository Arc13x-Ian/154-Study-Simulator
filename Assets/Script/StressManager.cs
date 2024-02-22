using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressManager : MonoBehaviour
{

    public float stress = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Stress Aura"))
        {
            Invoke("stressRising", 0.2f);
            Debug.Log("Stress at " + stress);
        }
    }

    void stressRising()
    {
        stress++;
    }
}
