using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameAddOns : MonoBehaviour
{
    //      movement and gameOver Reference 
    public bool canMove = true;
    public bool GameOver = false;
    public bool freeze;
    FPSController FPSController;

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
    public int AnxityLvl;
    public float AnxityEffectValue = 2.0f; // Set the Anxity effect rate
    public float anxityChangeDuration = 1.0f; // Set the duration for the Anxity effect change
    public bool Headphones = false; // a bool to turn off and on when the player has the headphones on
    private float HeadPhoneDuration = 5.0f;

    //      Tester bools for Methods
    private bool inRadius;
    public bool StudyDone;

    //       UI
    public Animator leftHandAnim;
    public Animator rightHandAnim;
    public bool EmptyLeftHand;
    public bool EmptyRightHand;
    public GameObject StudyScreen;
    public QuizManager QuizManager;
    public AnxityMeter AnxityMeter;


    // Start is called before the first frame update
    void Start()
    {

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

        StudyDone = true;
        StudyScreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Gameover Logic
        if (GameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOverScreen");
            FPSController.walkSpeed = 0.001f;
            FPSController.runSpeed = 0.0001f;
        }

        InteractionsIF();

        //if there are no more questions from a book it needs to take the player out of the UI study screen
        if (StudyDone == true)
        {
            asPlayer.Stop();
            StudyScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Stress Aura"))
        {

            StartCoroutine(StressCollide());
        }
    }

    IEnumerator StressCollide()
    {

        if (AnxityMeter.Anxietyscore <= 100)
        {
            AnxityMeter.Anxietyscore = AnxityMeter.Anxietyscore + 0.1f;
            Debug.Log("Stress at " + AnxityMeter.Anxietyscore);
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

    private void Study()
    {
        asPlayer.PlayOneShot(StudySound);
        StudyDone = false;
        StudyScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

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
                    asPlayer.Play();
                    return;
                }

                //we have the book in hand now

                else if (interactable != null && EmptyRightHand == false && interactable.CompareTag("Study Spot"))
                {
                    Debug.Log("Placing Book Down ");
                    //call this for when we are at the Study table to Spawn the Book on the table as well as to possibly move the character into study position

                    //interactable.putBookDown(EmptyLeftHand);

                    rightHandAnim.SetTrigger("putBookDown");
                    rightHandAnim.SetBool("HasBook", false);
                    EmptyRightHand = true;
                    Study();
                    asPlayer.Play();
                    return;

                }

                // if there is nothing to interact with then we do not do anything to it
                else if (interactable == null)
                {
                    Debug.Log("not an interactable");
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
                    EmptyLeftHand = false;
                    leftHandAnim.SetBool("HasHeadPhones", true);



                }

            }

        }


        //drinking tea function
        if (Input.GetKeyDown(KeyCode.E) && canMove && !GameOver && EmptyLeftHand == false && leftHandAnim.GetBool("HasTea"))
        {
            AnxityMeter.Anxietyscore = AnxityMeter.Anxietyscore - AnxityMeter.TeaRestore;
            TeaSound.Instance.PlayTeaDrinkingSound(); //James Edit this
            leftHandAnim.SetTrigger("DrinkTea");
            leftHandAnim.SetBool("HasTea", false);
            EmptyLeftHand = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && canMove && !GameOver && EmptyLeftHand == false && leftHandAnim.GetBool("HasHeadPhones"))
        {
            //set function to block the flow of Anxity for a set amout of time WIP
        }

    }

}
