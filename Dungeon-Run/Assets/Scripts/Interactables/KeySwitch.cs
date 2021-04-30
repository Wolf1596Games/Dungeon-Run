using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySwitch : MonoBehaviour
{
    public bool active = false;
    public Color activationColor;

    private SpriteRenderer sprRenderer;
    // Start is called before the first frame update
    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Key")
        {
            active = true;
            sprRenderer.color = activationColor;

            collision.transform.parent = null;
            Destroy(collision.gameObject);
        }
    }
}
