using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    private Rigidbody rb;
    public float jumpForce = 8f;         // Force applied when jumping
    public float moveSpeed = 5f;         // Speed of movement
    public float groundCheckDistance = 1.1f;  // Distance to check for ground

    private bool isGrounded;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;  // Ensures gravity is enabled
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        jumpAction.performed += _ => Jump();
    }

    void Update()
    {
        MovePlayer();
        GroundCheck();
    }

    void MovePlayer()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    void Jump()
    {
        if (isGrounded) // Only jump if grounded
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Debug.Log("Jumping!");
        }
        else
        {
            Debug.Log("Cannot jump; not grounded.");
        }
    }

    // Ground check using Raycast to ensure the player is near the ground
    void GroundCheck()
    {
        // Raycast downwards from the player's position
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance))
        {
            isGrounded = true;
            Debug.Log("Player is grounded.");
        }
        else
        {
            isGrounded = false;
            Debug.Log("Player is in the air.");
        }
    }

    // Alternative ground check using collision for reliability
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Ground detected on collision.");
        }
    }
}

