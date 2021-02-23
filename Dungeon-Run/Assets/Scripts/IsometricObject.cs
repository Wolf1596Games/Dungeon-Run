using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class IsometricObject : MonoBehaviour
{
    private const int IsometricRangePerYUnit = 20;

    [Tooltip("Will use this object to comput z-order")]
    public Transform target;

    [Tooltip("Use this to offset the object slightly in front of behind the Target object")]
    public int targetOffset = 0;

    private void Update()
    {
        if (target == null)
            target = transform;

        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingOrder = -(int)(target.position.y * IsometricRangePerYUnit) + targetOffset;
    }

}
