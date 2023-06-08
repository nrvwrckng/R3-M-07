// SpawnPoint.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool passedThrough = false;

    public bool HasPassedThrough()
    {
        return passedThrough;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            passedThrough = true;
        }
    }
}
