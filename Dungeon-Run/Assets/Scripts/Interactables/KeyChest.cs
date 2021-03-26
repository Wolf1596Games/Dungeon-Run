using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChest : MonoBehaviour
{
    public bool playerInRange = false;
    public float playerDetectionRange = 1f;
    public GameObject keyPrefab;

    private Transform player;
    private GameManager manager;

    // Update is called once per frame
    void Update()
    {
        if(manager == null)
        {
            manager = FindObjectOfType<GameManager>();
        }

        if(manager != null && player == null)
        {
            player = manager.activePlayer.transform;
        }

        if(player != null && Vector2.Distance(transform.position, player.position) <= playerDetectionRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Open();
        }
    }

    private void Open()
    {
        Instantiate(keyPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
