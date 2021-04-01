using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public bool visible = false;
    GameObject[] dialogueObjects;
    public Text dialogueText;
    public string startingDialogue;

    private void Start()
    {
        Time.timeScale = 1;
        dialogueObjects = GameObject.FindGameObjectsWithTag("Dialogue");
        HideDialogue();
    }

    public void ShowDialogue(string newText)
    {
        foreach(GameObject dObject in dialogueObjects)
        {
            Time.timeScale = 0;
            visible = true;
            dialogueText.text = newText;
            dObject.SetActive(true);
        }
    }

    public void HideDialogue()
    {
        foreach(GameObject dObject in dialogueObjects)
        {
            Time.timeScale = 1;
            visible = false;
            dObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(visible && Input.GetMouseButtonDown(0))
        {
            HideDialogue();
        }
    }
}
