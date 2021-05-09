using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemPlate : MonoBehaviour
{
    [Tooltip("Whether or not the pressure plate is active or not")]
    public bool active = false;
    public Sprite activatedPlate;
    public Sprite deactivatedPlate;

    SpriteRenderer sprRenderer;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Corpse")
        {
            active = true;
            sprRenderer.sprite = activatedPlate;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Corpse")
        {
            active = false;
            sprRenderer.sprite = deactivatedPlate;
        }
    }
}
