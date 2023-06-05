using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueUI;
    public GameObject interactionUI;
    public string[] dialogueLines;
    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionUI.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionUI.SetActive(false);
            playerInRange = false;
            dialogueUI.SetActive(false); // Hide the dialogue UI when leaving the trigger
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
            interactionUI.SetActive(false); // Hide the interaction UI when starting dialogue
        }
    }

    private void StartDialogue()
    {
        dialogueUI.SetActive(true); // Show the dialogue UI
        DialogueManager.instance.StartDialogue(dialogueLines);
    }
}
