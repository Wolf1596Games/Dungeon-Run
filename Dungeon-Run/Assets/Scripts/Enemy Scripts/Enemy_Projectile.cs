using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public Vector2 direction;
    public float lifetime;
    private float lifetimeCounter;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        lifetimeCounter = lifetime;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeCounter -= Time.deltaTime;
        if(lifetimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 inVelocity)
    {
        rb.velocity = inVelocity * speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<IsometricPlayerController>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }


}
