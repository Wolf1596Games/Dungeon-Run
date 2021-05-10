using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2 : Enemy
{
    public Transform projectile;
    public Transform target;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public float detectRad;
    public float shootTimer;
    public float inShootTimer;
    public float waitTime;
    public float startWaitTime;
    public bool isCooling = false;
    public bool accel = false;
    private Animator shooterAnim;
    public float speed;
    public Transform[] moveSpots;
    private int randomSpot;
    public GameObject bridgeSwitch;
    
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        shooterAnim = GetComponent<Animator>();
        currentState = EnemyState.idle;
        currentHealth = maxHealth;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
        Strafe();
        Accel();
        Death();
    }

    void Strafe()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= detectRad && isCooling == false)
        {
            Shoot();

        }
        else if (currentState == EnemyState.attack && isCooling)
        {
            Cooldown();
        }


    }

    void Accel()
    {
        if(currentHealth <= (maxHealth / 2) && accel == false)
        {
            speed += 2;
            inShootTimer = inShootTimer / 2;
            startWaitTime = startWaitTime / 2;
            accel = true;
        }
    }

    void Shoot()
    {
        ChangeState(EnemyState.attack);
        shooterAnim.SetBool("isShooting", true);
        shooterAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        shooterAnim.SetFloat("moveY", (target.position.y - transform.position.y));
    }
    void Cooldown()
    {

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            isCooling = false;
            shootTimer = inShootTimer;
            
        }
    }
    void TriggerArrow()
    {
        if (shootTimer == inShootTimer)
        {
            Transform arrow = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
            Transform arrow2 = Instantiate(projectile, spawnPoint2.transform.position, Quaternion.identity);
            Transform arrow3 = Instantiate(projectile, spawnPoint3.transform.position, Quaternion.identity);
            Vector3 shootDir = (spawnPoint.position - target.position).normalized;
            Vector3 shootDir2 = (spawnPoint2.position - target.position).normalized;
            Vector3 shootDir3 = (spawnPoint3.position - target.position).normalized;
            arrow.GetComponent<Enemy_Projectile>().Launch(shootDir);
            arrow2.GetComponent<Enemy_Projectile>().Launch(shootDir2);
            arrow3.GetComponent<Enemy_Projectile>().Launch(shootDir3);
            shooterAnim.SetBool("isShooting", false);
        }
        isCooling = true;
    }
    public void Death()
    {
        if (currentHealth <= 0)
        {
            bridgeSwitch.SetActive(true);
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
