using UnityEngine;

public class Movemint : MonoBehaviour
{
    Animator MovemintAnimator;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float secondJumpForce = 3f; // Jump force for the second jump
    public float deceleration = 10f;
    private float previousMoveX;
    private int jumpCount = 0; // Number of jumps performed
    private bool isJumping = false;
    private bool canDoubleJump = false; // Tracks if the player can perform a double jump
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MovemintAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        // Move horizontally
        if (moveX != 0)
        {
            // Flip the sprite if moving in the opposite direction
            spriteRenderer.flipX = moveX < 0;

            // Move horizontally
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

            // Check if the movement direction has changed
            if (Mathf.Sign(moveX) != Mathf.Sign(previousMoveX))
            {
                // Trigger the run animation immediately
                MovemintAnimator.Play("Run");
            }

            // Update the previous movement direction
            previousMoveX = moveX;
        }
        else
        {
            // Decelerate gradually if not moving
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0f, deceleration * Time.deltaTime), rb.velocity.y);

            // Play the idle animation
            MovemintAnimator.Play("Idle");

            // Reset the previous movement direction
            previousMoveX = 0f;
        }

        bool isMoving = Mathf.Abs(moveX) > 0;

        MovemintAnimator.SetBool("isMoving", isMoving);
        MovemintAnimator.SetBool("isJumping", isJumping);
        MovemintAnimator.SetLayerWeight(0, isMoving ? 1f : 0f); // Base layer
        MovemintAnimator.SetLayerWeight(1, isJumping ? 1f : 0f); // Jump layer

        if (isMoving)
        {
            Debug.Log("You are moving");
        }
        else
        {
            Debug.Log("NOT MOVING");
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isJumping)
            {
                Jump(jumpForce);
                canDoubleJump = true; // Set the flag for double jump
            }
            else if (canDoubleJump && jumpCount < 2) // Use canDoubleJump flag
            {
                Jump(secondJumpForce);
            }
        }
    }

    private void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset vertical velocity
        rb.AddForce(new Vector2(0f, force), ForceMode2D.Impulse);
        isJumping = true;
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            jumpCount = 0; // Reset the jump count when touching the ground
            canDoubleJump = false; // Reset the double jump flag
        }
    }
}
