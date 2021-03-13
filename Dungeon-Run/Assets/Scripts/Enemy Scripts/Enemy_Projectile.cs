using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    public int damage = 1;
    public float speed;
    public Vector3 direction;
    public float lifetime;
    private float lifetimeCounter;
    public Rigidbody2D rb;
    Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lifetimeCounter = lifetime;
        rb = GetComponent<Rigidbody2D>();
    }
   

    public void Launch(Vector3 shootDir)
    {
        this.direction = -shootDir;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(-shootDir));
    }
    void Update()
    {
        transform.position = transform.position + direction * Time.deltaTime * speed;
        lifetimeCounter -= Time.deltaTime;
        if (lifetimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
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
