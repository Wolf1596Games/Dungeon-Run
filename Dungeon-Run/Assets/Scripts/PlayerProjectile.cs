using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float lifespan = 10f;
    [SerializeField] float timeSinceCreated = 0f;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
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
            collision.gameObject.GetComponent<TestDummy>().TakeDamage(player.damage);

            Destroy(gameObject);
        }
    }
}
