using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : Enemy
{
    public Transform target;
    public float chaseRad;
    public Animator enemyAnim;
    private float inTimer;
    public bool isCooling = false;
    private int damage = 100;
    private IsometricPlayerController healthController;
    // Start is called before the first frame update
    void Awake()
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
        if (Vector2.Distance(target.position, transform.position) <= chaseRad)
        {
            FollowPlayer();
        }

    }

        void FollowPlayer()
    {
        if (currentState == EnemyState.idle || currentState == EnemyState.walk || currentState == EnemyState.attack || currentState == EnemyState.shoot)
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
    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<IsometricPlayerController>().TakeDamage(damage);
        }
    }
}
