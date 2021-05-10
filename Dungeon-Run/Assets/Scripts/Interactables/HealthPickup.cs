using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    GameManager manager;
    IsometricPlayerController activePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Player")
        {
            manager = FindObjectOfType<GameManager>();
            activePlayer = manager.activePlayer;
            if (activePlayer.currentHealth < activePlayer.maxHealth)
            {
                activePlayer.currentHealth += 1;

                activePlayer.healthSlider.value = activePlayer.currentHealth;

                Destroy(gameObject);
            }
        }
    }
}
