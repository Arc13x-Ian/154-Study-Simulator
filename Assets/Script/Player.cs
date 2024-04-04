using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Player Stats
    //      Speed
    public float walkingSpeed = 7.0f;
    public float runningSpeed = 11.0f;
    
    //      Jump height
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    //      Camera Stats
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    //      Player physics Reference
    public Rigidbody rb;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    //      movement and gameOver Reference 
    public bool canMove = true;
    public bool GameOver = false;
    public bool freeze;
    
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






    void Start()
    {


        characterController = GetComponent<CharacterController>();
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

    

    void Update()
    {

        // We are grounded, so re-calculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // movement math
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        InteractionsIF();


        //if there are no more questions from a book it needs to take the player out of the UI study screen
        if (StudyDone == true)
        {
            asPlayer.Stop();
            StudyScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        

       




            //Jumping Logic
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !GameOver)
        {
            moveDirection.y = jumpSpeed;

        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        //Gameover Logic
        if (GameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOverScreen");
        }

        //math for gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        //math for movement
        if (canMove && !GameOver)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {


            

            canMove = false;
            GameOver = true;
            Debug.Log("GameOver, you lose!");
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
        if (Action && canMove && characterController.isGrounded && !GameOver)
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
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
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


            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has an interactable component
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null && EmptyLeftHand == true && interactable.CompareTag("Tea"))
                {
                    interactable.ItemInteract(EmptyLeftHand);
                    EmptyLeftHand = false;
                    leftHandAnim.SetBool("HasTea", true);



                }

                if (interactable != null && EmptyLeftHand == true && interactable.CompareTag("HeadPhones"))
                {
                    interactable.ItemInteract(EmptyLeftHand);
                    EmptyLeftHand = false;
                    leftHandAnim.SetBool("HasTea", true);



                }

            }

        }


        //drinking tea function
        if (Input.GetKeyDown(KeyCode.E) && canMove && !GameOver && EmptyLeftHand == false && leftHandAnim.GetBool("HasTea"))
        {
            AnxityMeter.Anxietyscore = AnxityMeter.Anxietyscore - AnxityMeter.TeaRestore;
            asPlayer.clip = DrinkTea;
            asPlayer.Play();
            leftHandAnim.SetTrigger("DrinkTea");
            leftHandAnim.SetBool("HasTea", false);
            EmptyLeftHand = true;
        }
    }




}
