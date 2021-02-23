using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour
{
    [SerializeField] int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(FindObjectOfType<GameManager>().astralPlane == false)
            {
                Debug.Log("Attacked player");

                collision.gameObject.GetComponent<IsometricPlayerController>().TakeDamage(1);
            }
            else
            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }
    }
}
