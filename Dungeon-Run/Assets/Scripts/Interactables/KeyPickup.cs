using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public bool playerInRange = false;
    public float playerDetectionRange = 1f;

    private GameManager manager;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        player = manager.activePlayer.transform;

        if(Vector2.Distance(transform.position, player.position) <= playerDetectionRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if(playerInRange && Input.GetKeyDown(KeyCode.E) && transform.parent == null)
        {
            transform.parent = player;
        }
        else if(playerInRange && Input.GetKeyDown(KeyCode.E) && transform.parent != null)
        {
            transform.parent = null;
        }
    }
}
