using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float fallMultiplier = 2.5f; // Multiplier for faster falling
    private Rigidbody2D rb;
    private Animator animator;
    public float jumpForce = 10f;     // Initial jump force
    public float cutJumpMultiplier = 0.5f;  // Multiplier for jump cut when releasing jump button mid-air
    private bool isGrounded = false; // Track grounded status
    private bool isJumping;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 velocity = rb.linearVelocity;
        velocity.x = horizontalInput * speed;

        // Apply fall multiplier for faster falling
        if (velocity.y < 0)
        {
            velocity.y += Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        rb.linearVelocity = velocity;

        // Adjust player orientation
        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
        }

        // Set animation state
            animator.SetBool("walk", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded);

        // Handle jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
            debug.log("Player Jump")
        }

        // Handle jump cut (release jump button mid-air)
        if (Input.GetButtonUp("Jump") && isJumping && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * cutJumpMultiplier);
            isJumping = false;
        }

        // Reset jump state when landing
        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
