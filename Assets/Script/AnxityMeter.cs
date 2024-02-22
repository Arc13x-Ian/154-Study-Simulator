using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnxityMeter : MonoBehaviour
{
    private float score = 0f;
    public TextMeshProUGUI anxityScore;

    private float timePasted = 0f;
    public TextMeshProUGUI Timer;


    void Start()
    {
        // Invoke the function to increase score every 1 second (change the second parameter as needed)
        InvokeRepeating("IncreaseScore", 0f, 1f);
    }

    void Update()
    {
        anxityScore.text = score.ToString("0.0");
        timePasted += Time.deltaTime;
        Timer.text = timePasted.ToString("0:00");

    }

    void IncreaseScore()
    {
        // Increase the score by 0.2 points while sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            score += 0.2f;

        }
        //Increase the score by 0.1 points
        else
        {
            score += 0.1f;

        }
        Debug.Log("Current Score: " + score);
    }


}
