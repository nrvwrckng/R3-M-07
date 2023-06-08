using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Movemint playerController;

    private Vector3 initialPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<Movemint>();

        initialPosition = transform.position;
    }

    public void ResetPlayer()
    {
        // Reset player position
        transform.position = initialPosition;

        // Reset player velocity
        rb.velocity = Vector2.zero;

        // Reset player animations
        animator.Play("Idle"); // Replace "Idle" with the name of your idle animation state

        /*// Reset player state
        playerController.ResetState(); // Implement this method in your PlayerController script to reset any relevant player states*/
    }
}

