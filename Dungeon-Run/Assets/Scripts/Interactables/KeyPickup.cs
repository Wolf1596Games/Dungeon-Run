using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public bool playerInRange = false;
    public float playerDetectionRange = 1f;
    public AudioClip pickupSound;

    private GameManager manager;
    private AudioSource audioSource;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(pickupSound);
        }
        else if(playerInRange && Input.GetKeyDown(KeyCode.E) && transform.parent != null)
        {
            transform.parent = null;
            audioSource.PlayOneShot(pickupSound);
        }
    }
}
