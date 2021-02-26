using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool active = false;
    public bool previouslyActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine("Activation");
        }
    }

    private IEnumerator Activation()
    {
        active = true;

        yield return new WaitForEndOfFrame();

        active = false;
        previouslyActivated = true;
    }
}
