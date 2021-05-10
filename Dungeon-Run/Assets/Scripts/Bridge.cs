using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private PressurePlate activatorPlate;
    public bool raised = false;

    private SpriteRenderer sprRenderer;
    private Collider2D bridgeCollider;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        bridgeCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(activatorPlate != null && activatorPlate.active && !raised)
        {
            sprRenderer.color = new Color(sprRenderer.color.r, sprRenderer.color.g, sprRenderer.color.b, 1);
            bridgeCollider.isTrigger = true;
            raised = true;
        }
    }
}
