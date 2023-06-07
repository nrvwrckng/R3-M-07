using UnityEngine;

public class Movemint : MonoBehaviour
{
    Animator MovemintAnimator;
    public float moveSpeed = 5f;
    public float groundJumpForce = 5f; // Jump force for the ground jump
    public float airJumpForce = 3f; // Jump force for the mid-air jump
    public float deceleration = 10f;
    private float previousMoveX;
    private bool isJumping = false;
    private bool canGroundJump = false;
    private bool canAirJump = false;
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

            // Calculate the target velocity based on the move speed
            float targetVelocityX = moveX * moveSpeed;

            // Apply less horizontal movement if changing direction mid-air
            if (!isJumping && Mathf.Sign(moveX) != Mathf.Sign(previousMoveX))
            {
                targetVelocityX *= 0.5f;
            }

            // Move horizontally
            rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);

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

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (canGroundJump || canAirJump)
            {
                if (!isJumping && canGroundJump)
                {
                    Jump(groundJumpForce);
                    canGroundJump = false; // Disable ground jump after performing it
                    canAirJump = true; // Enable mid-air jump after performing ground jump
                }
                else if (canAirJump)
                {
                    Jump(airJumpForce);
                    canAirJump = false; // Disable mid-air jump after performing it
                }
            }
        }
    }


    private void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset vertical velocity
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isJumping = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            canGroundJump = true; // Enable ground jump after touching the ground
            canAirJump = false; // Disable mid-air jump after touching the ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canGroundJump = false; // Disable ground jump after leaving the ground
            canAirJump = true; // Enable mid-air jump after leaving the ground
        }
    }
}
