using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject Player;
    public void RetryLevel()
    {
        // Enable the GameOverScreen
        gameObject.SetActive(true);

        // Get the current scene index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1f;
    }

    public void GoToGameScene()
    {
        // Load the GameScene
        Time.timeScale = 1f;
        Player.SetActive(true);
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMainMenu()
    {
        // Load the MainMenuScene
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}



