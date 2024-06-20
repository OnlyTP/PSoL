using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 1f; 
    public float jumpForce = 2.6f; 
    private Vector2 lastPosition;

    private Animator anim;


    private Rigidbody2D body; 
    private SpriteRenderer sr; 

    public bool isGrounded; 
    public GameObject groundCheckPoint;
    public float groundCheckRadius; 
    public LayerMask groundLayer; 

    private bool jumpPressed = false; 
    private bool APressed = false; 
    private bool DPressed = false; 

    void Awake()
    {
        body = GetComponent<Rigidbody2D>(); 
        sr = GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>(); 
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
            LoadMainMenu(); 
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
