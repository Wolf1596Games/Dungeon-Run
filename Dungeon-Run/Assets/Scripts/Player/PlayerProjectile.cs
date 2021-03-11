using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Tooltip("Projectile's lifespan")]
    [SerializeField] float lifespan = 10f;
    [Tooltip("Time since the object was instantiated. FOR DEBUG ONLY")]
    [SerializeField] float timeSinceCreated = 0f;
    [Tooltip("Projectile velocity")]
    [SerializeField] float moveSpeed = 75f;

    IsometricPlayerController player;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<IsometricPlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceCreated += Time.deltaTime;

        if(timeSinceCreated >= lifespan)
        {
            //Destroy(gameObject);
        }
    }

    public void Setup(Vector3 shootDir)
    {
        rb.AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<TestDummy>().TakeDamage(player.damage);

            Destroy(gameObject);
        }
    }
}
