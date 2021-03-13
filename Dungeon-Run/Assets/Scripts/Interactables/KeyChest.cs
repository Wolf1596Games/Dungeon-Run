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
