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
           
            StartCoroutine(StressCollide());
        }
    }

    IEnumerator StressCollide()
    {

        if (stress <= 100)
        {
            
            stress++;
            Debug.Log("Stress at " + stress);
            yield return new WaitForSeconds(10);
        }
      
        
    }


    void stressRising()
    {
        stress++;
    }
}
