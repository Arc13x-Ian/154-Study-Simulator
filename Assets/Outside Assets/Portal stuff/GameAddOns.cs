using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using TMPro;


public class GameAddOns : MonoBehaviour
{
    //      movement and gameOver Reference 
    public bool canMove = true;
    public bool GameOver = false;
    public bool freeze;
    public FPSController FPSController;
    public bool GameWin = false;

    //      Animatoin and Audio
    public AudioClip JumpSound;
    public AudioClip DeathSound;

    public AudioClip BookPickUp;
    public AudioClip Pick;
    public AudioClip BookPutDown;
    public AudioClip StudySound;
    public AudioClip DrinkTea;
    public AudioClip heartbeatSound;


    private AudioSource asPlayer;
    private AudioSource heartbeatAudioSource;
    public Animator BorderIdle;

    //      Anxity Values for Game Mechanics
    public int AnxityLv1;
    public int AnxityLv2;
    public int AnxityLv3;
    public float AnxityEffectValue = 2.0f; // Set the Anxity effect rate
    public float anxityChangeDuration = 1.0f; // Set the duration for the Anxity effect change
    public bool Headphones = false; // a bool to turn off and on when the player has the headphones on
    private float HeadPhoneDuration = 4.0f;
    public bool TimeStop = false;
    public float TimeStopDuration;

    //      Tester bools for Methods
    private bool inRadius;
    public bool StudyDone;
    public bool isPlacingDown;

    //       UI
    public Animator leftHandAnim;
    public Animator rightHandAnim;
    public bool EmptyLeftHand;
    public bool EmptyRightHand;
    public GameObject StudyScreen;
    public PauseMenu pause;
    //public QuizManager QuizManager;
    public BookManager BookManager;

    public AnxityMeter AnxityMeter;
    public TextMeshProUGUI AnxietyText;


    // Start is called before the first frame update
    void Start()
    {
        //QuizManager = FindAnyObjectByType<QuizManager>();
        BookManager = FindAnyObjectByType<BookManager>();

        FPSController = FindAnyObjectByType<FPSController>();
        asPlayer = gameObject.AddComponent<AudioSource>();
        AnxityMeter = FindAnyObjectByType<AnxityMeter>();

        asPlayer.clip = Pick;

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //sets the hands empty at the start
        EmptyLeftHand = true;
        EmptyRightHand = true;
        //StudyDone = true;
        //StudyScreen.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

        
        //if the player wins!
        if (BookManager.BookScore >= 8)
        {
            GameWin = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canMove = false;
        }

        if(AnxityMeter.Anxietyscore >= AnxityLv1)
        {
            FPSController.walkSpeed = 13;
            FPSController.runSpeed = 17;
            StartCoroutine(ChangeFOVOverTime(1, 70));

        }
        if(AnxityMeter.Anxietyscore >= AnxityLv2)
        {
            FPSController.walkSpeed = 15;
            FPSController.runSpeed = 18;
            StartCoroutine(ChangeFOVOverTime(1, 80));

        }
        if (AnxityMeter.Anxietyscore >= AnxityLv3)
        {
            FPSController.walkSpeed = 18;
            FPSController.runSpeed = 21;
            StartCoroutine(ChangeFOVOverTime(1, 90));

        }
        if (AnxityMeter.Anxietyscore < AnxityLv1)
        {
            resetPlayerStats();
        }
        


        //Gameover Logic
        if (GameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canMove = false;
        }

        InteractionsIF();

        /*

        //if there are no more questions from a book it needs to take the player out of the UI study screen
        if (StudyDone == true && !pause.isPaused)
        {
            StudyScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canMove = true;
        }

        */

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Stress Aura") && !Headphones)
        {

            StartCoroutine(StressCollide());
        }
    }

    IEnumerator StressCollide()
    {

        if (AnxityMeter.Anxietyscore <= 100)
        {
            AnxityMeter.Anxietyscore = AnxityMeter.Anxietyscore + 0.1f;
            //Debug.Log("Stress at " + AnxityMeter.Anxietyscore);
            yield return new WaitForSeconds(10);
        }

    }

    public void AnxityChangeRate(bool Action, float AnxityEffect)
    {
        //area to Caluclate Anxity effects and mechanic
        if (Action && canMove && !GameOver)
        {
            BorderIdle.speed = AnxityEffect;
        }
        else
        {
            BorderIdle.speed = 1;
        }

    }

    /*

    private void Study()
    {
        Debug.Log("Study Atempt");
        asPlayer.PlayOneShot(StudySound);
        StudyDone = false;
        StudyScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        canMove = false;

    }
    */


    private void InteractionsIF()
    {
        //  EVERYTHING THAT WILL BE INTERACTABLE
        if (Input.GetMouseButtonDown(1) && canMove && !GameOver)
        {
            rightHandAnim.SetTrigger("rightHandPick");
            Ray ray = FPSController.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has an interactable component
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                //I pick up the book

                if (interactable != null && EmptyRightHand == true && interactable.CompareTag("Book"))
                {
                    // Call the interaction method on the interactable object

                    interactable.BookInteract(EmptyLeftHand);

                    rightHandAnim.SetBool("HasBook", true);
                    EmptyRightHand = false;
                    //StudyDone = false;
                    asPlayer.Play();
                    return;
                }

                //we have the book in hand now

                if (interactable != null && EmptyRightHand == false && interactable.CompareTag("Study Spot"))
                {
                    Debug.Log("Placing Book Down ");
                    //call this for when we are at the Study table to Spawn the Book on the table as well as to possibly move the character into study position

                    //interactable.putBookDown(EmptyLeftHand);

                    rightHandAnim.SetTrigger("putBookDown");
                    rightHandAnim.SetBool("HasBook", false);
                    //Study();
                    StartCoroutine(PuttingBookDown(1));
                    asPlayer.Play();
                    EmptyRightHand = true;
                    return;

                }

            }
        }

        //Left Hand Interactables!
        if (Input.GetMouseButtonDown(0) && canMove && !GameOver)
        {
            asPlayer.Play();

            leftHandAnim.SetTrigger("LeftHandPick");


            Ray ray = FPSController.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has an interactable component
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null && EmptyLeftHand == true && interactable.CompareTag("Tea"))
                {
                    Debug.Log("got tea");
                    interactable.ItemInteract(EmptyLeftHand);
                    leftHandAnim.SetBool("HasTea", true);
                    EmptyLeftHand = false;


                }

                if (interactable != null && EmptyLeftHand == true && interactable.CompareTag("HeadPhones"))
                {
                    interactable.ItemInteract(EmptyLeftHand);
                    leftHandAnim.SetBool("HasHeadPhones", true);
                    EmptyLeftHand = false;
                    //Debug.Log(interactable + " has been interacted with");


                }
                
                if (interactable != null && EmptyLeftHand == true && interactable.CompareTag("StopWatch"))
                {
                    interactable.ItemInteract(EmptyLeftHand);
                    leftHandAnim.SetBool("HasStopWatch", true);
                    EmptyLeftHand = false;
                    //Debug.Log(interactable + " has been interacted with");


                }

            }

        }


        //drinking tea function
        if (Input.GetKeyDown(KeyCode.E) && canMove && !GameOver && EmptyLeftHand == false && leftHandAnim.GetBool("HasTea"))
        {
            Debug.Log("tea was drank");
            AnxityMeter.Anxietyscore = AnxityMeter.Anxietyscore - AnxityMeter.TeaRestore;
            leftHandAnim.SetBool("HasTea", false);
            leftHandAnim.SetTrigger("DrinkTea");
            TeaSound.Instance.PlayTeaDrinkingSound(); //James Edit this
            EmptyLeftHand = true;
        }

        //drinking tea function
        if (Input.GetKeyDown(KeyCode.E) && canMove && !GameOver && EmptyLeftHand == false && leftHandAnim.GetBool("HasStopWatch"))
        {
            Debug.Log("Time Reverse!");
            //do time action here
            StartCoroutine(StopTimer());

            leftHandAnim.SetBool("HasStopWatch", false);
            leftHandAnim.SetTrigger("UseStopWatch");
            
            EmptyLeftHand = true;
        }


        if (Input.GetKeyDown(KeyCode.E) && canMove && !GameOver && EmptyLeftHand == false && leftHandAnim.GetBool("HasHeadPhones"))
        {

            Headphones = true;
            StartCoroutine(Deaf());
            
        }
        else
        {
            //Headphones = false;
        }

        if (!Headphones)
        {
            AnxietyText.color = Color.white;
        }

    }



    private IEnumerator Deaf()
    {
        leftHandAnim.SetTrigger("UseHeadPhones");
        leftHandAnim.SetBool("HasHeadPhones", false);
        EmptyLeftHand = true;
        AnxietyText.color = Color.blue;
        for (int i = 0; i < 11; i++)
        {
            
            if (i == HeadPhoneDuration)
            {
                Headphones = false;
                
            }
            yield return new WaitForSeconds(HeadPhoneDuration);
            //AnxietyText.color = Color.white;
        }
        
    }

    public IEnumerator StopTimer()
    {
        TimeStop = true;
        yield return new WaitForSeconds(TimeStopDuration);
        TimeStop = false;

    }

    //method to trigger a bool for a few seconds
    public IEnumerator PuttingBookDown(float seconds)
    {
        isPlacingDown = true;
        BookManager.placingBookDown();
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(seconds); // Wait for specified seconds
        Debug.Log("Coroutine finished after " + seconds + " seconds");
        isPlacingDown = false;
    }

    IEnumerator ChangeFOVOverTime(int duration, float targetFOV)
    {
        float timer = 0f;

        while (timer < duration)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Calculate the interpolation factor
            float t = Mathf.Clamp01(timer / duration);

            // Interpolate between the initial FOV and the target FOV
            FPSController.cam.fieldOfView = Mathf.Lerp(FPSController.cam.fieldOfView, targetFOV, t);

            // Wait for the end of frame before continuing the loop
            yield return null;
        }

        // Ensure the FOV is exactly the target value when the duration is finished
        FPSController.cam.fieldOfView = targetFOV;
    }

    private void resetPlayerStats()
    {
        FPSController.walkSpeed = 11;
        FPSController.runSpeed = 15;
        StartCoroutine(ChangeFOVOverTime(1,60));
        
    }

}
