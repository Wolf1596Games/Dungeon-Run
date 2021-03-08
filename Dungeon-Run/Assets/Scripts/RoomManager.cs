using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Tooltip("Radius in which to check for enemies (based on room size)")]
    [SerializeField] float checkRadius = 10f;

    [Tooltip("Enemies detected")]
    [SerializeField] int enemiesDetected = 0;

    TestDummy[] dummies;

    // Start is called before the first frame update
    void Start()
    {
        dummies = FindObjectsOfType<TestDummy>();

        foreach(TestDummy dummy in dummies)
        {
            if(Vector2.Distance(transform.position, dummy.transform.position) < checkRadius)
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
