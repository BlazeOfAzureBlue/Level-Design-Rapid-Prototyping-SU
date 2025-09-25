using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextInteractionManager : MonoBehaviour
{
    private float charactersPerSecond = 20;
    private bool textFinished = false;

    private string currentText;

    public GameObject TextCanvas;
    public TMP_Text dialogueText;
    private bool textactive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (textFinished)
            {
                TextCanvas.SetActive(false);
                textactive = false;
            }
            else
            {
                dialogueText.text = currentText;
                textFinished = true;
            }
        }
    }
    public void ActivateTypewriter(string TypeAnnouncement)
    {
        if(!textactive)
        {
            textactive = true;
            StartCoroutine(TypeText(TypeAnnouncement));
        }
    }
    IEnumerator TypeText(string TypeAnnouncement)
    {
        textFinished = false;
        TextCanvas.SetActive(true);
        currentText = TypeAnnouncement;
        string TypewriterText = null;
        foreach (char Character in TypeAnnouncement)
        {
            if (textFinished == false)
            {
                TypewriterText += Character;
                dialogueText.text = TypewriterText;
                yield return new WaitForSeconds(1 / charactersPerSecond);
            }
        }

        textFinished = true;
    }
}