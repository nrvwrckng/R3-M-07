using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject dialoguePanel; // Reference to your dialogue panel GameObject
    public Text dialogueText; // Reference to the Text component on your dialogue panel

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogWarning("DialogueManager instance already exists. Deleting duplicate...");
            Destroy(gameObject);
        }
    }

    public void StartDialogue(string[] dialogue)
    {
        // Display the dialogue panel
        dialoguePanel.SetActive(true);

        // Clear any previous text
        dialogueText.text = "";

        // Start showing the dialogue lines
        StartCoroutine(ShowDialogue(dialogue));
    }

    private IEnumerator ShowDialogue(string[] dialogue)
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogueText.text = dialogue[i];

            // Wait for a brief duration before showing the next line
            yield return new WaitForSeconds(5f); // Adjust the duration as per your preference
        }

        // End of dialogue
        EndDialogue();
    }

    public void EndDialogue()
    {
        // Hide the dialogue panel
        dialoguePanel.SetActive(false);

        // Perform any necessary actions after dialogue ends
    }
}


