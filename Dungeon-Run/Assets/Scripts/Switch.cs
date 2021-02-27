using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Tooltip("Whether or not the player is in range")]
    public bool playerInRange = false;
    [Tooltip("Whether or not the switch is active")]
    public bool active = false;

    private SpriteRenderer sprRenderer;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
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
