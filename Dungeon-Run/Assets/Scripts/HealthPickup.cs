using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    IsometricPlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<IsometricPlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Player")
        {
            if (player.currentHealth < player.maxHealth)
            {
                player.currentHealth += 1;

                Destroy(gameObject);
            }
        }
    }
}
