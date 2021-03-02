using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1 : Enemy
{

    public Transform target;
    public float chaseRad;
    public float atkRad;
    public float timer;
    public Transform homePos;
    private Animator enemyAnim;
    private float inTimer;
    public bool isCooling = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        inTimer = timer;

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
                isCooling = false;
                ChangeState(EnemyState.walk);

                
            }
           

        }
         if ((currentState == EnemyState.walk || currentState == EnemyState.attack) && Vector3.Distance(target.position, transform.position) <= atkRad && isCooling == false)
        {
            Attack();

        }
       else if (currentState == EnemyState.attack && isCooling == true)
        {
            
            Cooldown();
        }

    }

    void Attack()
    {
        timer = inTimer;
        ChangeState(EnemyState.attack);
        enemyAnim.SetBool("isMoving", false);
        enemyAnim.SetBool("isAttacking", true);
    }


    void Cooldown()
    {
        enemyAnim.SetBool("isAttacking", false);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            isCooling = false;
            timer = inTimer;
        }
    }

    void TriggerCD()
    {
        isCooling = true;
    }

  
    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
