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

    private string[] currentDialogue;
    private int currentLineIndex = 0;
    private bool isDialogueActive = false;
    private Coroutine textAnimationCoroutine;

    public void StartDialogue(string[] dialogue)
    {
        if (isDialogueActive)
            return;

        currentDialogue = dialogue;
        dialoguePanel.SetActive(true);
        dialogueText.text = "";

        currentLineIndex = 0;
        isDialogueActive = true;
        DisplayCurrentLine();
    }

    private void DisplayCurrentLine()
    {
        if (currentLineIndex < currentDialogue.Length)
        {
            if (textAnimationCoroutine != null)
                StopCoroutine(textAnimationCoroutine);

            textAnimationCoroutine = StartCoroutine(AnimateText(currentDialogue[currentLineIndex]));
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator AnimateText(string dialogue)
    {
        dialogueText.text = ""; // Clear the text initially

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogueText.text += dialogue[i]; // Append one character at a time

            yield return new WaitForSeconds(0.1f); // Adjust the delay between characters as per your preference
        }

        textAnimationCoroutine = null;
    }

    public void DisplayNextLine()
    {
        if (!isDialogueActive)
            return;

        currentLineIndex++;
        DisplayCurrentLine();
    }

    public void EndDialogue()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
    }
}
