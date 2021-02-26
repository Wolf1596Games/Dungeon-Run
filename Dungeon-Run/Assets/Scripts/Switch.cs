using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool playerInRange = false;
    public bool active = false;

    private SpriteRenderer sprRenderer;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();

        sprRenderer.color = Color.yellow;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E) && !active)
        {
            active = true;
            sprRenderer.color = Color.green;
        }
        else if(playerInRange && Input.GetKeyDown(KeyCode.E) && active)
        {
            active = false;
            sprRenderer.color = Color.yellow;
        }
    }
}
