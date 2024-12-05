/* 
Opens menu on specific button

Works Remaining:        - NA     
Problems:               - Loop Dialouge
                        - making it a press button makes it to loop forever
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject dialougeCanvas;

    public bool isInDialouge = false;

    PlayerInput playerInput;
    NPCDialouge npcDialouge;
    PickUpItem pickUp;
    Save save;

    // Initialize menu as close
    private void Start()
    {
        menuCanvas.SetActive(false);
        dialougeCanvas.SetActive(false);
    }

    // if specific button is pressed menu button is opened (checks constantly)
    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        npcDialouge = FindNearestNPCWithDialogueComponent(GameObject.FindWithTag("Player").transform.position);
        Debug.Log(npcDialouge.name);

        pickUp = FindObjectOfType<PickUpItem>();

        if (currentScene.buildIndex == 1) 
        {
            Menu();

            Dialouge();

            ItemDialouge();
        } else
        {
            menuCanvas.SetActive(false);
        }
    }

    public void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }

    public void Dialouge()
    {
        if (Input.GetKeyDown(KeyCode.Z) && npcDialouge.playerIsClose && !isInDialouge)
        {
            Debug.Log("axa");
            StartCoroutine(StartDialogue());
        }
    }

    public void ItemDialouge()
    {
        if (Input.GetKeyDown(KeyCode.Z) && pickUp.playerIsClose && !isInDialouge)
        {
            StartCoroutine(StartDialogueItem());
            save = FindObjectOfType<Save>();
            save.MediKit();
        }
    }

    // Start the dialogue coroutine
    IEnumerator StartDialogue()
    {
        while (npcDialouge.index < npcDialouge.dialogue.Length)
        {
            isInDialouge = true; 

            npcDialouge.dialoguePanel.SetActive(true); 

            npcDialouge.Typing(); 

            npcDialouge.dialogueText.text = npcDialouge.dialogue[npcDialouge.index];

            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
            yield return new WaitForSeconds(1.0f);

            npcDialouge.NextLine();
        }

        isInDialouge = false;
        npcDialouge.zeroText();
    }

    IEnumerator StartDialogueItem()
    {
        while (pickUp.index < pickUp.dialogue.Length)
        {
            isInDialouge = true;

            pickUp.dialoguePanel.SetActive(true);

            pickUp.Typing();

            pickUp.dialogueText.text = pickUp.dialogue[pickUp.index];

            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
            yield return new WaitForSeconds(1.0f);

            pickUp.NextLine();
        }

        isInDialouge = false;
        pickUp.zeroText();
    }


    public static NPCDialouge FindNearestNPCWithDialogueComponent(Vector3 playerPosition)
    {
        // Get all NPCDialouge components in the scene
        NPCDialouge[] npcs = FindObjectsOfType<NPCDialouge>();

        NPCDialouge nearestNPC = null;
        float nearestDistance = Mathf.Infinity;

        foreach (NPCDialouge npc in npcs)
        {
            // Calculate the distance from the player to this NPC
            float distance = Vector3.Distance(playerPosition, npc.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestNPC = npc;
            }
        }

        return nearestNPC; 
    }
}

