using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToLevel : MonoBehaviour
{
    private GameManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager = FindObjectOfType<GameManager>();
        if(collision.tag == "Player")
        {
            manager.FromAstralPlane();
        }
    }
}
