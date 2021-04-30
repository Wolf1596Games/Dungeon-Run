using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private PressurePlate activatorPlate;
    public bool raised = false;

    private SpriteRenderer sprRenderer;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(activatorPlate != null && activatorPlate.active && !raised)
        {
            sprRenderer.color = new Color(sprRenderer.color.r, sprRenderer.color.g, sprRenderer.color.b, 1);
            raised = true;
        }
    }
}
