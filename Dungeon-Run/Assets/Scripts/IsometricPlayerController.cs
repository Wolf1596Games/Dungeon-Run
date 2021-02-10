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

    //Private fighting variables
    private float timeSinceSwing = .35f;
    private float timeSinceShot = .35f;
    private bool facingLeft = false;
    private bool facingRight = false;

    //Other private variables
    private bool movingLeft = false;
    private bool movingRight = true;

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
        //Movement
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

    private void Update()
    {
        //Increment time variables
        timeSinceDodge += Time.deltaTime;
        timeSinceSwing += Time.deltaTime;
        timeSinceShot += Time.deltaTime;

        //Get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        //Determine which way the player should fire
        if (mousePos.x <= transform.position.x)
        {
            facingLeft = true;
            facingRight = false;
        }
        else
        {
            facingRight = true;
            facingLeft = false;
        }

        //Determine which way the player is facing
        if (rb.velocity.x < 0)
        {
            movingLeft = true;
            movingRight = false;
        }
        else if (rb.velocity.x > 0)
        {
            movingLeft = false;
            movingRight = true;
        }

        //Sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprinting = true;

            speed *= sprintMultiplier;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;

            speed = baseMovementSpeed;
        }

        

        //Melee Attack
        if (Input.GetButtonDown("Fire1") && timeSinceSwing >= timeBetweenSwings)
        {
            MeleeAttack();
        }
        //Ranged Attack
        if (Input.GetButtonDown("Fire2") && timeSinceShot >= timeBetweenShots)
        {
            RangedAttack();
        }
    }

    //Attacking
    public void MeleeAttack()
    {
        TestDummy[] dummies = FindObjectsOfType<TestDummy>();

        foreach (TestDummy dummy in dummies)
        {
            //If the dummy is within swingRange, attack
            if (Vector2.Distance(transform.position, dummy.transform.position) <= swingRange)
            {
                //Check to make sure the dummy is on the correct side
                if (facingLeft == true && dummy.transform.position.x <= transform.position.x)
                {
                    dummy.TakeDamage(damage);
                }
                else if (facingRight == true && dummy.transform.position.x > transform.position.x)
                {
                    dummy.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("No enemy detected");
                }
            }
        }

        timeSinceSwing = 0f;
    }
    public void RangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;

        if (facingLeft == true)
        {
            projectile.GetComponent<Rigidbody2D>().velocity = Vector2.left * projectileSpeed;
        }
        else
        {
            projectile.GetComponent<Rigidbody2D>().velocity = Vector2.right * projectileSpeed;
        }

        timeSinceShot = 0f;
    }

    //Taking damage
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            main.transform.SetParent(null);

            Destroy(gameObject);
        }
    }
}
