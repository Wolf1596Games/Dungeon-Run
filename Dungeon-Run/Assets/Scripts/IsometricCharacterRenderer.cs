using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
    public static readonly string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static readonly string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    Animator animator;
    int lastDirection;

    private void Awake()
    {
        //cache the animator component
        animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 direction)
    {
        //use the Run states by default
        string[] directionArray = null;

        //measure the magnitude of the input
        if(direction.magnitude < .01f)
        {
            //if we are basically standing still, we'll use the Static states
            directionArray = staticDirections;
        }
        else
        {
            directionArray = runDirections;
            //lastDirection = DirectionToIndex(direction, 8);
        }

        animator.Play(directionArray[lastDirection]);
    }
}
