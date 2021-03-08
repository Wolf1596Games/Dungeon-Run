using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    IsometricPlayerController player;
    public int damage = 1;
    public float timeBetweenDamaging = 5f;
    public float slowMultiplier = .65f;
    private float timeSinceDamaged = 0f;
    private bool playerInAcid = false;

    

    private void Update()
    {
        timeSinceDamaged += Time.deltaTime;

        if(playerInAcid && timeSinceDamaged >= timeBetweenDamaging)
        {
            timeSinceDamaged = 0f;

            player.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerInAcid = true;
            player = collision.GetComponent<IsometricPlayerController>();

            player.currentSpeed *= slowMultiplier;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerInAcid = false;
            player.currentSpeed /= slowMultiplier;
            player = null;
        }
    }
}
