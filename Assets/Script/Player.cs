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
    public GameObject Weapon;
    public AudioClip JumpSound;
    public AudioClip DeathSound;
    private AudioSource asPlayer;
    public Animator animator;
    public Animator BorderIdle;

    public float AnxityLV1 = 2.0f; // Set the Anxity level
    public float anxityChangeDuration = 1.0f; // Set the duration for the Anxity level change


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        asPlayer = GetComponent<AudioSource>();
        
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
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


        Anxity(isRunning);
        if (isRunning)
        {
            animator.SetFloat("Speed", runningSpeed);
        }
        if (!isRunning)
        {
            animator.SetFloat("Speed", walkingSpeed);
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
        // note for review consider establishing the player's collider with otherand then set tags to the triggers!@!
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
            
            animator.SetTrigger("Death");
            asPlayer.PlayOneShot(DeathSound, 1.0f);
            canMove = false;
            GameOver = true;
            Debug.Log("GameOver, you lose!");
        }
    }

    public void Anxity(bool Action)
    {
        //area to Caluclate Anxity effects and mechanic
        if (Action && canMove && characterController.isGrounded && !GameOver)
        {
            BorderIdle.speed = AnxityLV1;
        }
        else
        {
            BorderIdle.speed = 1;
        }


    }

    public void AAnxity(bool action)
    {
        if (action && canMove && characterController.isGrounded && !GameOver)
        {
            StartCoroutine(ChangeAnxityLevelSmooth(AnxityLV1, anxityChangeDuration));
        }
        else
        {
            StartCoroutine(ChangeAnxityLevelSmooth(1.0f, anxityChangeDuration));
        }
    }

    IEnumerator ChangeAnxityLevelSmooth(float targetAnxityLevel, float duration)
    {
        float currentAnxityLevel = BorderIdle.speed;
        float timer = 0f;

        while (timer < duration)
        {
            BorderIdle.speed = Mathf.Lerp(currentAnxityLevel, targetAnxityLevel, timer / duration);
            timer += Time.deltaTime;
            yield return null;
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
