using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] float baseMovementSpeed = 8f;
    [SerializeField] float speed = 0f;
    [SerializeField] float sprintMultiplier = 1.75f;
    [SerializeField] float dodgeDuration = .5f;
    [SerializeField] float dodgeCooldown = 5f;
    [SerializeField] bool sprinting = false;

    [Header("Player Health")]
    [SerializeField] public int maxHealth = 3;
    [SerializeField] public int currentHealth = 3;

    [Header("Player Fighting")]
    [SerializeField] public int damage = 1;
    [SerializeField] float swingRange = 1.5f;
    [SerializeField] float timeBetweenSwings = .35f;
    [SerializeField] float timeBetweenShots = .2f;

    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 6.5f;

    //Private movement variables
    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;
    private float timeSinceDodge = 5f;

    //References
    private IsometricCharacterRenderer isoRenderer;
    private Rigidbody2D rb;
    private Camera main;

    private void Awake()
    {
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        rb = GetComponent<Rigidbody2D>();
        main = FindObjectOfType<Camera>();

        //Set speed to base speed
        speed = baseMovementSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * speed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rb.MovePosition(newPos);
    }
}
