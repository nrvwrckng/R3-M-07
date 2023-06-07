using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene("GameScene");
    }

    public void HowToPlay()
    {
        // Load the settings scene or open a settings panel
        SceneManager.LoadScene("ControlsScene");
    }

    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }
}

