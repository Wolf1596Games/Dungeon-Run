using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Tooltip("Projectile's lifespan")]
    public float lifespan = 10f;
    [Tooltip("Time since the object was instantiated. FOR DEBUG ONLY")]
    public float timeSinceCreated = 0f;
    [Tooltip("Projectile velocity")]
    public float moveSpeed = 75f;

    GameManager manager;
    IsometricPlayerController activePlayer;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        activePlayer = manager.activePlayer;
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

    public void Setup(Vector3 shootDir)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);

        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(activePlayer.damage);

            Destroy(gameObject);
        }
        else if(collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
