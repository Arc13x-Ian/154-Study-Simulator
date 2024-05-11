using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Rendering.PostProcessing;

public class AnxityMeter : MonoBehaviour
{
    public float score = 0f;

    public float Anxietyscore = 0f;
    public TextMeshProUGUI anxityScore;
    public float TeaRestore;

    private float timePasted = 0f;
    public TextMeshProUGUI MinTimer;
    public TextMeshProUGUI SecTimer;
    public TextMeshProUGUI AMPMTimer;

    public GameAddOns Player;

    public int seconds = 0;
    public int min = 10;
    Coroutine Timerate;
    Color TimerColor;

    public GameObject[] Books = null;
    public TextMeshProUGUI totalBooks;
    public TextMeshProUGUI UsedBooks;

    public PPtesting PostProcessEffect;

    void Start()
    {
        // Invoke the function to increase score every 1 second (change the second parameter as needed)
        StartCoroutine(IncreaseScore());

        //the amount of books in level
        Books = GameObject.FindGameObjectsWithTag("Book");
        totalBooks.text = Books.Length.ToString("/0");


        //coroutine
        Timerate = StartCoroutine(TimeCounter());


        //we get the player script from somewhere from the scene
        Player = FindAnyObjectByType<GameAddOns>();

        //finds Our Post Processing Volume
        PostProcessEffect = FindAnyObjectByType<PPtesting>();


        TimerColor = MinTimer.color;

    }

    void Update()
    {

        anxityScore.text = Anxietyscore.ToString("0.0");
        if (Anxietyscore < 0f)
        {
            Anxietyscore = 0;

        }


        if (Anxietyscore > 30)
        {
            PostProcessEffect.frequency = 6f;
            PostProcessEffect.maxIntensity = 0.79f;
            
        }
        if (Anxietyscore > 60)
        {
            PostProcessEffect.frequency = 6.5f;
            PostProcessEffect.maxIntensity = 0.82f;
        }
        if(Anxietyscore > 90)
        {
            PostProcessEffect.frequency = 7f;
            PostProcessEffect.maxIntensity = 0.9f;
        }


        if (Anxietyscore > 100f)
        {
            Anxietyscore = 100f;
        }

        if (min == 5 && seconds == 59 || Anxietyscore >= 100)
        {
            //end game
            Player.GameOver = true;
            Player.canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //SceneManager.LoadScene("GameOverScreen");

        }

        // Create a list to store valid GameObjects
        List<GameObject> validBooks = new List<GameObject>();

        // Iterate through the Books array
        for (int i = 0; i < Books.Length; i++)
        {
            // Check if the GameObject is not null
            if (Books[i] != null)
            {
                // Add the GameObject to the validBooks list
                validBooks.Add(Books[i]);
            }
        }

        // Convert the validBooks list back to an array
        Books = validBooks.ToArray();

        UsedBooks.text = Books.Length.ToString("0");

    }

    private IEnumerator IncreaseScore()
    {
        while (Anxietyscore < 100.01f)
        {
            float Index = 0.1f;

            // Increase the score by 0.2 points while sprinting
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Index = 0.2f;

            }
            //Increase the score by 0.1 points

            if (Player.Headphones)
            {
                Index = 0f;
                yield return new WaitForSeconds(5);
            }
            else if (!Player.Headphones)
            {
                Anxietyscore += Index;
                yield return new WaitForSeconds(1);

            }
            //Debug.Log("Current Score: " + score);
        }

    }
    public void ModifyAnxiety(float amount)
    {
        score += amount;
    }

    private IEnumerator TimeCounter()
    {
        while (seconds <= 59)
        {
            if (Player.TimeStop)
            {
                MinTimer.color = Color.cyan;
                SecTimer.color = Color.cyan;
                AMPMTimer.color = Color.cyan;

                    yield return new WaitForSeconds(Player.TimeStopDuration);
            }


            seconds++;
            if (seconds == 60)
            {
                min++;
                seconds = 00;
                if (min == 13)
                {
                    min = 1;
                    AMPMTimer.text = "pm";


                }



            }


            MinTimer.color = new Color(0.8392158f, 0.1921569f, 0.3019608f);
            SecTimer.color = new Color(0.8392158f, 0.1921569f, 0.3019608f);
            AMPMTimer.color = new Color(0.8392158f, 0.1921569f, 0.3019608f);
            MinTimer.text = min.ToString("0");
            SecTimer.text = seconds.ToString("00");
            yield return new WaitForSeconds(1);
        }
    }




}
