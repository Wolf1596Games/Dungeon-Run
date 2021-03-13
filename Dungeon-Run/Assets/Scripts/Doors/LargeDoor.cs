using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeDoor : MonoBehaviour
{
    public KeySwitch switch1;
    public KeySwitch switch2;
    public string animTriggerName;

    private Collider2D collider2D;
    private SpriteRenderer sprRenderer;

    private void Awake()
    {
        collider2D = GetComponentInChildren<Collider2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(switch1.active && switch2.active)
        {
            collider2D.isTrigger = true;
            sprRenderer.color = new Color(sprRenderer.color.r, sprRenderer.color.g, sprRenderer.color.b, 0);
        }
    }
}
