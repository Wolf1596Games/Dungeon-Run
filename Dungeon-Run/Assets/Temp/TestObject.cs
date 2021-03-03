using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    public PressurePlate activatorPlate;
    public Switch activatorSwitch;

    void Update()
    {
        if(activatorPlate != null)
        {
            if (activatorPlate.active && !activatorPlate.previouslyActivated)
            {
                StartCoroutine("Move");
            }
        }
        else
        {
            if(activatorSwitch.active)
            {
                StartCoroutine("Move");
            }
        }
    }

    private IEnumerator Move()
    {
        transform.Translate(0, 5, 0);

        yield return new WaitForEndOfFrame();
    }
}
