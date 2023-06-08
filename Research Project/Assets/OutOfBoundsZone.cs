// OutOfBoundsZone.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsZone : MonoBehaviour
{
    private bool alreadyCollided = false;
    public float collisionCooldown = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!alreadyCollided && other.CompareTag("Player"))
        {
            alreadyCollided = true;

            // Teleport the player to the nearest spawn point
            TeleportPlayerToSpawnPoint(other.gameObject);

            // Deduct a life from the player
            DeductLife(other.gameObject);

            // Start the cooldown for subsequent collisions
            StartCollisionCooldown();
        }
    }

    private void TeleportPlayerToSpawnPoint(GameObject player)
    {
        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
        SpawnPoint nearestSpawnPoint = GetNearestSpawnPoint(spawnPoints, player.transform.position);

        if (nearestSpawnPoint != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.SetSpawnPoint(nearestSpawnPoint.transform.position);
                playerHealth.ResetPlayerPosition();
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on the player.");
            }
        }
    }

    private void DeductLife(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.DeductLife();
        }
        else
        {
            Debug.LogWarning("PlayerHealth component not found on the player.");
        }
    }

    private void StartCollisionCooldown()
    {
        // Reset the alreadyCollided flag after the specified duration
        Invoke("ResetCollisionCooldown", collisionCooldown);
    }

    private void ResetCollisionCooldown()
    {
        alreadyCollided = false;
    }

    private SpawnPoint GetNearestSpawnPoint(SpawnPoint[] spawnPoints, Vector3 playerPosition)
    {
        SpawnPoint nearestSpawnPoint = null;
        float nearestDistance = Mathf.Infinity;

        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint.HasPassedThrough())
            {
                float distance = Vector3.Distance(playerPosition, spawnPoint.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestSpawnPoint = spawnPoint;
                }
            }
        }

        return nearestSpawnPoint;
    }
}
