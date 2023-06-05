using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text text;
    public float delay = 0.1f;

    private string fullText;
    private string currentText = "";
    private int currentCharacter = 0;
    private bool isAnimating = false;

    public void StartTypewriter(string newText)
    {
        if (isAnimating)
        {
            StopCoroutine(TypewriterCoroutine());
        }

        fullText = newText;
        currentText = "";
        currentCharacter = 0;
        isAnimating = true;

        StartCoroutine(TypewriterCoroutine());
    }

    private IEnumerator TypewriterCoroutine()
    {
        while (currentCharacter < fullText.Length)
        {
            currentText += fullText[currentCharacter];
            text.SetText(currentText);
            currentCharacter++;

            yield return new WaitForSeconds(delay);
        }

        isAnimating = false;
    }
}
