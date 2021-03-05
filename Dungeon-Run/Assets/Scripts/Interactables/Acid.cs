using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    GameManager manager;
    IsometricPlayerController activePlayer;
    public int damage = 1;
    public float timeBetweenDamaging = 5f;
    private float timeSinceDamaged = 0f;
    private bool playerInAcid = false;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        activePlayer = manager.activePlayer;
    }

    private void Update()
    {
        timeSinceDamaged += Time.deltaTime;

        if(playerInAcid && timeSinceDamaged >= timeBetweenDamaging)
        {
            timeSinceDamaged = 0f;

            activePlayer.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerInAcid = true;

            activePlayer.slowed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerInAcid = false;

            activePlayer.slowed = false;
        }
    }
}
