using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float springForce = 10f; // The force applied to the player when activated

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 launchDirection = Vector2.up; // Change this if you want a different launch direction
                rb.velocity = Vector2.zero; // Reset the player's velocity
                rb.AddForce(launchDirection * springForce, ForceMode2D.Impulse); // Apply the spring force
            }
        }
    }
}

