using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float walkingSpeed = 7.0f;
    public float runningSpeed = 11.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public Rigidbody rb;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public bool canMove = true;
    public bool GameOver = false;
    public bool freeze;
    
    public AudioClip JumpSound;
    public AudioClip DeathSound;
    private AudioSource asPlayer;
    public Animator CameraAnimator;
    public Animator BorderIdle;

    public int AnxityLvl;
    public float AnxityEffectValue = 2.0f; // Set the Anxity effect rate
    public float anxityChangeDuration = 1.0f; // Set the duration for the Anxity level change

    public bool EmptyLeftHand;
    public bool EmptyRightHand;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        asPlayer = GetComponent<AudioSource>();
        
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //sets the hands empty at the start
        EmptyLeftHand = true;
        EmptyRightHand = true;
        
    }

    void Update()
    {

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        AnxityChangeRate(isRunning, AnxityEffectValue);
        if (isRunning)
        {
            CameraAnimator.SetFloat("Speed", runningSpeed);
        }
        if (!isRunning)
        {
            CameraAnimator.SetFloat("Speed", walkingSpeed);
        }




        if (Input.GetMouseButtonDown(0) && canMove && !GameOver)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has an interactable component
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                //I pick up the book

                if (interactable != null && EmptyLeftHand == true)
                {
                    // Call the interaction method on the interactable object
                    interactable.BookInteract(EmptyLeftHand);
                    EmptyLeftHand = false;
                }

                //we have the book in hand now

                if(interactable != null && EmptyLeftHand == false)
                {
                    //call this for when we are at the Study table to Spawn the Book on the table as well as to possibly move the character into study position
                }


                if (interactable == null)
                {
                    Debug.Log("not an interactable");
                }

            }
        }




        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !GameOver)
        {
            moveDirection.y = jumpSpeed;
            asPlayer.PlayOneShot(JumpSound, 1.0f);
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (GameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove && !GameOver)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // note for review consider establishing the player's collider with "other" and then set tags for the triggers!@!
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {


            CameraAnimator.SetTrigger("Death");
            asPlayer.PlayOneShot(DeathSound, 1.0f);
            canMove = false;
            GameOver = true;
            Debug.Log("GameOver, you lose!");
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

    public void Anxity(bool Action, int AnxityLevel)
    {
        if (Action)
        {
            AnxityLvl = AnxityLvl + AnxityLevel;
        }
    }

    
    

    private void pickUpBook()
    {

    }

    private void putdownBook()
    {

    }

    private void Study()
    {

    }

}
