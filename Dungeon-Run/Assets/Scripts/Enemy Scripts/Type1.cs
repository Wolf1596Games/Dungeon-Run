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
    void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        inTimer = timer;
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Death();
        CheckDistance();
        
    }

    void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRad && Vector2.Distance(target.position, transform.position) > atkRad)
        {
            FollowPlayer();
        }

        else if (Vector3.Distance(target.position, transform.position) > chaseRad)
        {
            ResetPosition();
        }
        if ((currentState == EnemyState.walk || currentState == EnemyState.attack) && Vector2.Distance(target.position, transform.position) <= atkRad && isCooling == false)
        {
            Attack();

        }
       else if (currentState == EnemyState.attack && isCooling == true)
        {
            Cooldown();
        }

    }

    void FollowPlayer()
    {
        if (currentState == EnemyState.idle || currentState == EnemyState.walk || currentState == EnemyState.attack)
        {
            enemyAnim.SetBool("isMoving", true);
            enemyAnim.SetBool("isAttacking", false);
            enemyAnim.SetFloat("moveX", (target.position.x - transform.position.x));
            enemyAnim.SetFloat("moveY", (target.position.y - transform.position.y));
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            isCooling = false;
            ChangeState(EnemyState.walk);
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

    public void ResetPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, homePos.position, moveSpeed * Time.deltaTime);
        enemyAnim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        enemyAnim.SetFloat("moveY", (homePos.position.y - transform.position.y));

        if (transform.position == homePos.position)
        {
            enemyAnim.SetBool("isMoving", false);
            ChangeState(EnemyState.idle) ;
        }
    }

    public void Death()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
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
