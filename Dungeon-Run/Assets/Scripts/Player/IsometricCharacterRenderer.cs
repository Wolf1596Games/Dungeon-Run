using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
    public static readonly string[] staticDirections = { "Static W", "Static E" };
    public static readonly string[] runDirections = { "Run W", "Run E" };

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
            lastDirection = DirectionToIndex(direction, 2);
        }

        //To be implemented once animations are in place
        //animator.Play(directionArray[lastDirection]);
    }

    //this function converts a Vector2 direction to an index to a slice around a circle
    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        //get the normalized direction
        Vector2 normDir = dir.normalized;

        //calculate how many degrees one slice is
        float step = 360f / sliceCount;

        //calculate how many degrees half a slice is
        float halfStep = step / 2;

        //get the angle from -180 to 180 of the direction vector relative to the Up vector
        float angle = Vector2.SignedAngle(Vector2.up, normDir);

        //add the halfslice offset
        angle += halfStep;

        //if angle is negative, then let's make it positiove by adding 3160 to wrap it around.
        if(angle < 0)
        {
            angle += 360;
        }

        //calculate the amount of steps required to reach this angle
        float stepCount = angle / step;

        //round it, and we have the answer
        return Mathf.FloorToInt(stepCount);
    }
}
