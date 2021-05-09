using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrection : MonoBehaviour
{
    private GameManager manager;
    private bool active = false;
    public Sprite activatedPortal;

    [SerializeField] private Switch[] switches;

    private void Update()
    {
        if(CountActiveSwitches() == switches.Length)
        {
            Activate();
        }
    }

    private void Activate()
    {
        active = true;
        GetComponent<SpriteRenderer>().sprite = activatedPortal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager = FindObjectOfType<GameManager>();

        if (collision.tag == "Player" && active)
        {
            manager.FromAstralPlane();
        }
    }

    private int CountActiveSwitches()
    {
        int activeSwitches = 0;

        foreach(Switch s in switches)
        {
            if(s.active)
            {
                activeSwitches++;
            }
        }

        return activeSwitches;
    }
}
