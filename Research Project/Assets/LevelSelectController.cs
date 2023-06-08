using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        // Check if each level is accessible and update the button interactability accordingly
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isLevelAccessible = CheckIfLevelAccessible(i + 1); // Assuming level indices start from 1

            // Set the button interactability based on level accessibility
            levelButtons[i].interactable = isLevelAccessible;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        // Load the selected level
        SceneManager.LoadScene("Level" + levelIndex);
    }

    public void DebugLevel()
    {
        SceneManager.LoadScene("DebugLevel");
    }

    public int GetCurrentLevelIndex()
    {
        // Get the current level index by extracting it from the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;
        string levelIndexString = currentSceneName.Substring(currentSceneName.Length - 1); // Assumes level names end with the level index
        int levelIndex;
        if (int.TryParse(levelIndexString, out levelIndex))
        {
            return levelIndex;
        }
        else
        {
            Debug.LogWarning("Invalid level index!");
            return -1;
        }
    }

    private bool CheckIfLevelAccessible(int levelIndex)
    {
        // Check if the previous level is completed (assumed to be stored somewhere)
        bool isPreviousLevelCompleted = IsPreviousLevelCompleted(levelIndex);

        // Level 1 is always accessible, and subsequent levels require the previous level to be completed
        if (levelIndex == 1 || isPreviousLevelCompleted)
        {
            return true; // Level is accessible
        }
        else
        {
            return false; // Level is not accessible
        }
    }

    private bool IsPreviousLevelCompleted(int levelIndex)
    {
        // Implement your own logic to determine if the previous level (levelIndex - 1) is completed
        // You can use PlayerPrefs, a data manager, or any other method to store and retrieve the completion status of levels
        // Return true if the previous level is completed, or false otherwise
        // Example: return PlayerPrefs.GetInt("Level" + (levelIndex - 1) + "Completed", 0) == 1;
        return PlayerPrefs.GetInt("Level" + (levelIndex - 1) + "Completed", 0) == 1;
    }
}
