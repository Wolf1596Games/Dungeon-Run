using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Type1
{
    public Transform dropPoint;
    public GameObject corpsePrefab;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    void SpawnCorpse()
    {
        if (currentHealth <= 0)
        {
            GameObject corpse = Instantiate(corpsePrefab, dropPoint.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCorpse();
    }
  
}
