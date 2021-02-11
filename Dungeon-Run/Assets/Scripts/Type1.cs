using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1 : Enemy
{

    public Transform target;
    public float chaseRad;
    public float atkRad;
    public Transform homePos;
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRad && Vector3.Distance(target.position, transform.position) > atkRad)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                ChangeState(EnemyState.walk);
            }
            
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
