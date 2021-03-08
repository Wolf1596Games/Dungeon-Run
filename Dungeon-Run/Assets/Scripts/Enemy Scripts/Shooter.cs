using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    public GameObject projectile;
    public Transform target;
    public float detectRad;
    public float timer;
    private float inTimer = 3;
    public bool isCooling;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= detectRad && isCooling == false)
        {
            ChangeState(EnemyState.attack);
            Vector2 tempVector = target.transform.position - transform.position;
            GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
            isCooling = true;
            current.GetComponent<Enemy_Projectile>().Launch(tempVector);
        }
        else if (isCooling)
        {
            Cooldown();
        }

    }
    void Cooldown()
    {
        //enemyAnim.SetBool("isAttacking", false);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            isCooling = false;
            timer = inTimer;
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
