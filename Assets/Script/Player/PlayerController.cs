using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 1f; // Running speed.
    public float jumpForce = 2.6f; // Jump height.
    private Vector2 lastPosition; // last pos 

    public Sprite jumpSprite; // Sprite that shows up when the character is not on the ground. [OPTIONAL]

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

    }

    // Update() is called every frame.
    void Update()
    {
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        APressed = Input.GetKey(KeyCode.A); 
        DPressed = Input.GetKey(KeyCode.D); 


        if (Input.GetKeyDown(KeyCode.Escape)) LoadMainMenu(); // Load Main menu.




        // Left/Right movement.
        if (APressed)
        {
            body.velocity = new Vector2(-runSpeed * Time.deltaTime, body.velocity.y); // Move left physics.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z); // Rotating the character object to the left.
        }
        else if (DPressed)
        {
            body.velocity = new Vector2(runSpeed * Time.deltaTime, body.velocity.y); // Move right physics.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z); // Rotating the character object to the right.
        }
        else body.velocity = new Vector2(0, body.velocity.y);

        //  =====================TODO GET RID OF DOUBLE JUMPING ==========
        // Jumps.
        if (jumpPressed && isGrounded)
        {

            body.velocity = new Vector2(0, jumpForce); // Jump physics.


        }
        lastPosition = gameObject.transform.position;

    }



    
    void FixedUpdate()
    {
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
