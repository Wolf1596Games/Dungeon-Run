using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    private GameManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            manager = FindObjectOfType<GameManager>();

            manager.ToNextLevel();
        }
    }
}
