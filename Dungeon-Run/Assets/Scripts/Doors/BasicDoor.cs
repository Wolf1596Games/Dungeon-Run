using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDoor : MonoBehaviour
{
    [SerializeField] private Switch activatorSwitch;
    [SerializeField] private PressurePlate activatorPlate;
    [SerializeField] private string triggerName;

    private Collider2D collider2d;
    private Animator animator;

    private void Awake()
    {
        collider2d = GetComponentInChildren<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activatorSwitch != null && activatorSwitch.active)
        {
            collider2d.isTrigger = true;
            animator.SetTrigger(triggerName);
        }
        else if(activatorPlate != null && activatorPlate.active)
        {
            collider2d.isTrigger = true;
            animator.SetTrigger(triggerName);
        }
    }
}
