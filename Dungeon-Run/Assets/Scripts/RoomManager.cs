using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Tooltip("Radius in which to check for enemies (based on room size)")]
    [SerializeField] float checkRadius = 10f;

    [Tooltip("Enemies detected")]
    [SerializeField] int enemiesDetected = 0;

    Enemy[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = FindObjectsOfType<Enemy>();

        foreach(Enemy enemy in enemies)
        {
            if(Vector2.Distance(transform.position, enemy.transform.position) < checkRadius)
            {
                enemiesDetected += 1;
            }
        }
    }

    public void EnemyKilled()
    {
        enemiesDetected -= 1;
    }
}
