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

    public void Setup(Vector3 shootDir)
    {
        Debug.Log("Setting up projectile");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Debug.Log("Adding force of " + (shootDir * moveSpeed));
        rb.AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);

        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<TestDummy>().TakeDamage(player.damage);

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
