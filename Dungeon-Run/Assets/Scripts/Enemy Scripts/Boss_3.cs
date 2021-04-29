using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_3 : Enemy
{
    public Transform projectile;
    public Transform spawnPoint;
    public float detectRad;
    public float shootTimer;
    public float inShootTimer;
    public bool shootCooling = false;
    public Transform target;
    public float chaseRad;
    public float atkRad;
    public float timer;
    public Transform homePos;
    private Animator enemyAnim;
    private float inTimer;
    public bool isCooling = false;
    public Switch switch1;
    public Switch switch2;
    public Switch switch3;
    public bool isHealthMax = false;

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
        HealthRegen();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= detectRad && Vector3.Distance(target.position, transform.position) > chaseRad && shootCooling == false)
        {
            Shoot();
        }
        else if (currentState == EnemyState.shoot && shootCooling)
        {
            ShootCooldown();
        }
        else if (Vector2.Distance(target.position, transform.position) <= chaseRad && Vector2.Distance(target.position, transform.position) > atkRad)
        {
            FollowPlayer();
        }

        else if (Vector3.Distance(target.position, transform.position) > detectRad)
        {
            shootCooling = false;
            ResetPosition();
            
        }
        else if ((currentState == EnemyState.walk || currentState == EnemyState.attack) && Vector2.Distance(target.position, transform.position) <= atkRad && isCooling == false)
        {
            Attack();

        }
        else if (currentState == EnemyState.attack && isCooling)
        {
            Cooldown();
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
            ChangeState(EnemyState.idle);
        }
    }

    public void Death()
    {
        if (currentHealth <= 0 && (switch1.active && switch2.active && switch3.active))
        {
            Destroy(gameObject);
        }
        else if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    void Shoot()
    {
        ChangeState(EnemyState.shoot);
        enemyAnim.SetBool("isShooting", true);
        enemyAnim.SetBool("isMoving", false);
        enemyAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        enemyAnim.SetFloat("moveY", (target.position.y - transform.position.y));
    }
    void ShootCooldown()
    {

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootCooling = false;
            shootTimer = inShootTimer;
        }
    }
    void TriggerArrow()
    {
        if (shootTimer == inShootTimer)
        {
            Transform arrow = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
            Vector3 shootDir = (spawnPoint.position - target.position).normalized;
            arrow.GetComponent<Enemy_Projectile>().Launch(shootDir);
            enemyAnim.SetBool("isShooting", false);
        }
        shootCooling = true;
    }

    void HealthRegen()
    {
        if(isHealthMax == false && switch1.active && switch2.active && switch3.active)
        {
            currentHealth = maxHealth;
            isHealthMax = true;
        }
    }
    

}
