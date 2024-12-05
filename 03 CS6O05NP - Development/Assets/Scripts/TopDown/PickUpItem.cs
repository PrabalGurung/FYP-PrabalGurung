using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public GameObject item;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    public int index;
    public bool playerIsClose;
    public float wordSpeed;

    public GameObject DialoguePanel
    {
        get { return dialoguePanel; }
        set { dialoguePanel = value; }
    }
    public TextMeshProUGUI DialogueText
    {
        get { return dialogueText; }
        set { dialogueText = value; }
    }
    public string[] Dialogue
    {
        get { return dialogue; }
        set { dialogue = value; }
    }
    public int Index
    {
        get { return index; }
        set { index = value; }
    }
    public bool PlayerIsClose
    {
        get { return playerIsClose; }
        set { playerIsClose = value; }
    }
    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    public void Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length)
        {
            index++;
            dialogueText.text = "";
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;

            Destroy(item);

            zeroText();
        }
    }
}
