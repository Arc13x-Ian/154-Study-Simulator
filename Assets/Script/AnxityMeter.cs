using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnxityMeter : MonoBehaviour
{
    public float score = 0f;
    public TextMeshProUGUI anxityScore;
    public float TeaRestore;

    private float timePasted = 0f;
    public TextMeshProUGUI MinTimer;
    public TextMeshProUGUI SecTimer;

    public Player Player;

    public int seconds = 0;
    public int min = 0;
    Coroutine Timerate;

    void Start()
    {
        // Invoke the function to increase score every 1 second (change the second parameter as needed)
        InvokeRepeating("IncreaseScore", 0f, 1f);


        //coroutine
        Timerate = StartCoroutine(TimeCounter());


        //we get the player script from somewhere from the scene
        Player = FindAnyObjectByType<Player>();

    }

    void Update()
    {
        anxityScore.text = score.ToString("0.0");

        if (min == 5 && seconds == 59 || score >= 100)
        {
            //end game
            Player.GameOver = true;
            Player.canMove = false;

        }


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
        //Debug.Log("Current Score: " + score);
    }

    private IEnumerator TimeCounter()
    {
        
        while (seconds <= 59)
        {
            seconds++;
            if (seconds == 60)
            {
                min++;
                seconds = 00;

                
            }

           
            
            MinTimer.text = min.ToString("0");
            SecTimer.text = seconds.ToString(":00");
            yield return new WaitForSeconds(1);
        }

    }




}
