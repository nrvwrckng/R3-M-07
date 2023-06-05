using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

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
        dialoguePanel.SetActive(true);
        dialogueText.text = "";

        StartCoroutine(ShowDialogue(dialogue));
    }

    private IEnumerator ShowDialogue(string[] dialogue)
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogueText.text = "";
            yield return StartCoroutine(AnimateText(dialogue[i]));
            yield return new WaitForSeconds(2f); // Wait for a brief duration before showing the next line
        }

        EndDialogue();
    }

    private IEnumerator AnimateText(string dialogue)
    {
        dialogueText.text = ""; // Clear the text initially

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogueText.text += dialogue[i]; // Append one character at a time

            yield return new WaitForSeconds(0.1f); // Adjust the delay between characters as per your preference
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        // Perform any necessary actions after dialogue ends
    }
}
