using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 1f; // Running speed.
    public float jumpForce = 2.6f; // Jump height.
    private Vector2 lastPosition; // last pos 

    private Animator anim;


    private Rigidbody2D body; // Variable for the RigidBody2D component.
    private SpriteRenderer sr; // Variable for the SpriteRenderer component.

    public bool isGrounded; // Variable that will check if character is on the ground.
    public GameObject groundCheckPoint; // The object through which the isGrounded check is performed.
    public float groundCheckRadius; // isGrounded check radius.
    public LayerMask groundLayer; // Layer wich the character can jump on.

    private bool jumpPressed = false; // Variable that will check is "Space" key is pressed.
    private bool APressed = false; // Variable that will check is "A" key is pressed.
    private bool DPressed = false; // Variable that will check is "D" key is pressed.

    void Awake()
    {
        body = GetComponent<Rigidbody2D>(); // Setting the RigidBody2D component.
        sr = GetComponent<SpriteRenderer>(); // Setting the SpriteRenderer component.
        anim = GetComponent<Animator>(); // Get the Animator component attached to this GameObject.
    }

    // Update() is called every frame.
    void Update()
    {
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        APressed = Input.GetKey(KeyCode.A);
        DPressed = Input.GetKey(KeyCode.D);

        // Handle game exit to main menu.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu(); // Call the function to load the main menu.
        }

        // Handle left/right movement.
        if (APressed)
        {
            // Move left
            body.velocity = new Vector2(-runSpeed, body.velocity.y); // Update the body's velocity to move left.
            transform.eulerAngles = new Vector3(0, 180, 0); // Flip the character to face left.
            anim.SetBool("isRunning", true); // Trigger the running animation.
        }
        else if (DPressed)
        {
            // Move right
            body.velocity = new Vector2(runSpeed, body.velocity.y); // Update the body's velocity to move right.
            transform.eulerAngles = new Vector3(0, 0, 0); // Flip the character to face right (default orientation).
            anim.SetBool("isRunning", true); // Trigger the running animation.
        }
        else
        {
            // No left/right movement
            body.velocity = new Vector2(0, body.velocity.y); // Stop horizontal movement.
            anim.SetBool("isRunning", false); // Stop the running animation.
        }

        // Handle jumping.
        if (jumpPressed && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            anim.SetBool("isJumping", true); // Trigger the jumping animation
        }
        else if (!isGrounded)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        // Track the last position of the player.
        lastPosition = transform.position;
    }




    void FixedUpdate()
    {
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
