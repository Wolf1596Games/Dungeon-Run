using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDoor : MonoBehaviour
{
    [SerializeField] private GolemPlate activatorPlate;
    [SerializeField] private string triggerName;

    private Collider2D collider2d;
    private SpriteRenderer sprRenderer;
    //private Animator animator;

    private void Awake()
    {
        collider2d = GetComponentInChildren<Collider2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activatorPlate != null && activatorPlate.active)
        {
            collider2d.isTrigger = true;
            sprRenderer.color = new Color(sprRenderer.color.r, sprRenderer.color.g, sprRenderer.color.b, 0);
            //animator.SetTrigger(triggerName);
        }
        else
        {
            collider2d.isTrigger = false;
            sprRenderer.color = new Color(sprRenderer.color.r, sprRenderer.color.g, sprRenderer.color.b, 1);
        }
    }
}
