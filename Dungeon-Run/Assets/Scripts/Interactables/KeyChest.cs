using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChest : MonoBehaviour
{
    public bool playerInRange = false;
    public float playerDetectionRange = 1f;
    public GameObject keyPrefab;
    public AudioClip openingFX;
    public float openingTIme = 1f;

    private Transform player;
    private AudioSource audioSource;
    private GameManager manager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            audioSource.PlayOneShot(openingFX);
            StartCoroutine("Opening");
        }
    }

    private IEnumerator Opening()
    {
        yield return new WaitForSeconds(openingTIme);

        Instantiate(keyPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
