using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1 : Enemy
{

    public Transform target;
    public float chaseRad;
    public float atkRad;
    public Transform homePos;
    private Animator enemyAnim;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
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
            if(currentState == EnemyState.idle || currentState == EnemyState.walk || currentState == EnemyState.attack)
            {
                enemyAnim.SetBool("isMoving", true);
                enemyAnim.SetBool("isAttacking", false);
                enemyAnim.SetFloat("moveX", (target.position.x - transform.position.x));
                enemyAnim.SetFloat("moveY", (target.position.y - transform.position.y));
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                ChangeState(EnemyState.walk);
                if ((currentState == EnemyState.walk) && Vector3.Distance(target.position, transform.position) <= atkRad)
                {

                    enemyAnim.SetBool("isMoving", false);
                    enemyAnim.SetBool("isAttacking", true);
                    ChangeState(EnemyState.attack);
                }

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
