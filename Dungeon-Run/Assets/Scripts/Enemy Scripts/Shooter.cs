using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    public Transform projectile;
    public Transform target;
    public Transform spawnPoint;
    public float detectRad;
    public float timer;
    public float inTimer;
    public bool isCooling = false;
    private Animator shooterAnim;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        shooterAnim = GetComponent<Animator>();
        currentState = EnemyState.idle;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
        Death();
    
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= detectRad && isCooling == false)
        {
            Attack();
            
        }
        else if (currentState == EnemyState.attack && isCooling)
        {
            Cooldown();
        }
       

    }

    void Attack()
    {
        ChangeState(EnemyState.attack);
        shooterAnim.SetBool("isAttacking", true);
        shooterAnim.SetFloat("aimX", (target.position.x - transform.position.x));
        shooterAnim.SetFloat("aimY", (target.position.y - transform.position.y));
    }
    void Cooldown()
    {
        
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isCooling = false;
            timer = inTimer;
        }
    }
    void TriggerArrow()
    { 
        if (timer == inTimer)
        {
            Transform arrow = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
            Vector3 shootDir = (spawnPoint.position - target.position).normalized;
            arrow.GetComponent<Enemy_Projectile>().Launch(shootDir);
            shooterAnim.SetBool("isAttacking", false);
        }
        isCooling = true;
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
