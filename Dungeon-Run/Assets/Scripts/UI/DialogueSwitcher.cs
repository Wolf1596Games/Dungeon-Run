using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSwitcher : MonoBehaviour
{
    GameManager manager;
    DialogueManager dManager;
    [TextArea(3, 5)]
    [Tooltip("Text to be displayed")]
    public string dialogueText;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        dManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            dManager.ShowDialogue(dialogueText);
        }
    }
}
