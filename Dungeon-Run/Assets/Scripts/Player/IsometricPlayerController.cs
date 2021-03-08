using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [Tooltip("Base player movement speed")]
    [SerializeField] float baseMovementSpeed = 2f;
    [Tooltip("Current player movement speed")]
    [SerializeField] public float currentSpeed = 0f;
    [Tooltip("Multiplier for sprinting")]
    [SerializeField] float sprintMultiplier = 1.75f;
    [Tooltip("Duration of the player's dodge")]
    [SerializeField] float dodgeDuration = .5f;
    [Tooltip("Player's dodge cooldown")]
    [SerializeField] float dodgeCooldown = 5f;
    [Tooltip("Multiplier for dodging")]
    [SerializeField] float dodgeMultiplier = 2.5f;
    [Tooltip("Shows whether the player is sprinting or not. FOR DEBUG ONLY")]
    [SerializeField] bool sprinting = false;

    [Header("Player Health")]
    [Tooltip("Player's max health")]
    [SerializeField] public int maxHealth = 6;
    [Tooltip("Player's current health")]
    [SerializeField] public int currentHealth = 6;

    [Header("Player Combat")]
    [Tooltip("Player's damage per hit")]
    [SerializeField] public int damage = 1;
    [Tooltip("Player's melee swing range")]
    [SerializeField] float swingRange = 1.5f;
    [Tooltip("Time between player's melee attacks")]
    [SerializeField] float timeBetweenSwings = .35f;
    [Tooltip("Time between player's ranged attacks")]
    [SerializeField] float timeBetweenShots = .2f;

    [Header("Projectile")]
    [Tooltip("Prefab object for projectiles")]
    [SerializeField] GameObject projectilePrefab;
    [Tooltip("Projectile's velocity when instantiated")]
    [SerializeField] float projectileSpeed = 6.5f;

    public bool isActivePlayer = false;

    //Private movement variables
    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;
    private float timeSinceDodge = 5f;

    //Private fighting variables
    private float timeSinceSwing = .35f;
    private float timeSinceShot = .35f;
    private bool facingLeft = false;
    private bool facingRight = false;

    //References
    //Player GameObject MUST have both this controller script and the IsometricCharacterRenderer
    private IsometricCharacterRenderer isoRenderer;
    private Rigidbody2D rb;
    private Camera main;
    private GameManager manager;

    private Vector2 movement;

    private void Awake()
    {
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        rb = GetComponent<Rigidbody2D>();
        main = FindObjectOfType<Camera>();
        manager = FindObjectOfType<GameManager>();

        //Set speed to base speed
        currentSpeed = baseMovementSpeed;
    }

    private void Update()
    {
        if(isActivePlayer)
        {
            //Increment time variables
            timeSinceDodge += Time.deltaTime;
            timeSinceSwing += Time.deltaTime;
            timeSinceShot += Time.deltaTime;

            //Assign x and y values of movement
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

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

            //Sprinting
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                sprinting = true;

                currentSpeed *= sprintMultiplier;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                sprinting = false;

                currentSpeed = baseMovementSpeed;
            }

            //Dodging
            if (Input.GetButtonDown("Dodge") && timeSinceDodge >= dodgeCooldown)
            {
                StartCoroutine("DodgeCoroutine");
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
    }

    private void FixedUpdate()
    {
        if (isActivePlayer)
        {
            //Movement
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime); 
        }
    }

    private IEnumerator DodgeCoroutine()
    {
        currentSpeed *= dodgeMultiplier;
        timeSinceDodge = 0f;

        yield return new WaitForSeconds(dodgeDuration);

        currentSpeed /= dodgeMultiplier;
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
            Death();
        }
    }
    public void Death()
    {
        main.transform.SetParent(null);

        //Destroy(gameObject);
        if(!manager.astralPlane)
        {
            manager.astralPlane = true;
            manager.ToAstralPlane();
        }
    }
}
