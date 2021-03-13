using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Tooltip("Projectile's lifespan")]
    [SerializeField] float lifespan = 10f;
    [Tooltip("Time since the object was instantiated. FOR DEBUG ONLY")]
    [SerializeField] float timeSinceCreated = 0f;

    IsometricPlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<IsometricPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceCreated += Time.deltaTime;

        if(timeSinceCreated >= lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(player.damage);

            Destroy(gameObject);
        }
    }
}
