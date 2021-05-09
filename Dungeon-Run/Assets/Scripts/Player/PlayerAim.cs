using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    [Tooltip("Delay between when shots can be fired")]
    [SerializeField] float timeBetweenShots = .2f;

    private float timeSinceLastShot = .2f;


    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private IsometricPlayerController controller;
    private Animator aimAnimator;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        controller = GetComponent<IsometricPlayerController>();
        aimAnimator = aimTransform.GetComponentInChildren<Animator>();

        timeSinceLastShot = timeBetweenShots;
    }

    private void Update()
    {
        if (controller.isActivePlayer)
        {
            timeSinceLastShot += Time.deltaTime;

            HandleAiming();
            HandleShooting();
        }
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDir = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void HandleShooting()
    {
        if(Input.GetButtonDown("Fire2") && timeSinceLastShot >= timeBetweenShots)
        {
            timeSinceLastShot = 0f;

            Vector3 mousePosition = GetMouseWorldPosition();

            aimAnimator.SetTrigger("Shoot");
            OnShoot?.Invoke(this, new OnShootEventArgs{
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
