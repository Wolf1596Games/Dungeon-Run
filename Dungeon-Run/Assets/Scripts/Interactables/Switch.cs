using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Tooltip("Whether or not the player is in range")]
    public bool playerInRange = false;
    [Tooltip("Whether or not the switch is active")]
    public bool active = false;
    public AudioClip flipNoise;
    public Sprite activatedSwitch;
    public Sprite deactivatedSwitch;

    private SpriteRenderer sprRenderer;
    private AudioSource audioSource;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(flipNoise);
            sprRenderer.sprite = activatedSwitch;
        }
        else if(playerInRange && Input.GetKeyDown(KeyCode.E) && active)
        {
            active = false;
            audioSource.PlayOneShot(flipNoise);
            sprRenderer.sprite = deactivatedSwitch;
        }
    }
}
