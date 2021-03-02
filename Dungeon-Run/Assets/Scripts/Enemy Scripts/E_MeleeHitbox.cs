using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MeleeHitbox : MonoBehaviour
{
    public int damage;
    private IsometricPlayerController healthController;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<IsometricPlayerController>().TakeDamage(damage);
        }
    }

    
}
