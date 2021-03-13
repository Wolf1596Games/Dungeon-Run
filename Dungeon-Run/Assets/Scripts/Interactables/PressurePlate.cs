﻿using System.Collections;
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

    SpriteRenderer sprRenderer;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
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
        sprRenderer.color = Color.green;

        yield return new WaitForEndOfFrame();

        active = false;
        previouslyActivated = true;
    }
    private IEnumerator Deactivation()
    {
        active = false;
        sprRenderer.color = Color.yellow;

        yield return new WaitForEndOfFrame();

        previouslyActivated = false;
    }
}
