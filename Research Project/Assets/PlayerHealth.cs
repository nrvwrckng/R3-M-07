using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public TMP_Text livesText;
    public GameObject gameOverScreen;
    public GameObject Player;
    private Vector3 currentSpawnPoint;

    private int currentLives;

    private void Start()
    {
        currentLives = maxLives;
        UpdateLivesText();
    }

    public void DeductLife()
    {
        currentLives--;
        UpdateLivesText();
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake != null)
        {
            cameraShake.Shake();
        }


        if (currentLives <= 0)
        {
            ShowGameOverScreen();
        }
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        UpdateLivesText();
    }

    public void SetSpawnPoint(Vector3 spawnPoint)
    {
        currentSpawnPoint = spawnPoint;
    }

    public void ResetPlayerPosition()
    {
        transform.position = currentSpawnPoint;
    }

    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + currentLives;
    }

    private void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        Player.SetActive(false);
    }
}
