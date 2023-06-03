using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string[] dialogueLines;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision Detected");
            DialogueManager.instance.StartDialogue(dialogueLines);
            // You can add additional actions like disabling player movement during dialogue
            // or triggering other events based on your game design
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collision Gone");
            DialogueManager.instance.EndDialogue();
        }
    }
}

