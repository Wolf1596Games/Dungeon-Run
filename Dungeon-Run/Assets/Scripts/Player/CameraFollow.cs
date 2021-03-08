using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offset;
    private Vector3 playerPosition;
    public float offsetSmoothing;

    private void Update()
    {
        playerPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        if(target.localScale.x > 0f)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y + offset, playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y - offset, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing + Time.deltaTime);
    }
}
