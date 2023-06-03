using UnityEngine;

public class Movemint : MonoBehaviour
{
    Animator MovemintAnimator;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float deceleration = 10f;
    private float previousMoveX;
    private bool isJumping = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MovemintAnimator = this.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

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

        if (isMoving == true)
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
            Jump();
        }
    }

    private void Jump()
    {
        if (!isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
