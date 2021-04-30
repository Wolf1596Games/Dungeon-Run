using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Tooltip("Whether or not the pressure plate is active or not")]
    public bool active = false;
    [Tooltip("Whether or not the pressure plate has been activated before or not")]
    public bool previouslyActivated = false;
    [Tooltip("Sets whether the player can activate it or not")]
    public bool playerCanActivate = true;
    public AudioClip activationNoise;
    public Color activationColor;
    public Color deactivationColor;

    SpriteRenderer sprRenderer;
    private AudioSource audioSource;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerCanActivate && collision.tag == "Player")
        {
            StartCoroutine("Activation");
        }
        else if(!playerCanActivate && collision.tag == "Corpse" || collision.tag == "Enemy")
        {
            StartCoroutine("Activation");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!playerCanActivate && collision.tag == "Corpse" || collision.tag == "Enemy")
        {
            StartCoroutine("Deactivation");
        }
    }

    private IEnumerator Activation()
    {
        active = true;
        sprRenderer.color = activationColor;
        audioSource.PlayOneShot(activationNoise);

        yield return new WaitForEndOfFrame();

        active = false;
        audioSource.PlayOneShot(activationNoise);
        previouslyActivated = true;
    }
    private IEnumerator Deactivation()
    {
        active = false;
        sprRenderer.color = deactivationColor;

        yield return new WaitForEndOfFrame();

        previouslyActivated = false;
    }
}
