using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    shoot,
    stagger,
    corpse

}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public int maxHealth;
    public int currentHealth;
    public float moveSpeed;

    // Start is called before the first frame update

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        GetComponent<AudioSource>().Play();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
