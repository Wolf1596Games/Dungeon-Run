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

    [Header("Projectile")]
    [Tooltip("Prefab object for projectiles")]
    [SerializeField] Transform projectilePrefab;

    public bool isActivePlayer = false;

    //Private movement variables
    private float timeSinceDodge = 5f;

    //Private fighting variables
    private float timeSinceSwing = .35f;
    private bool facingLeft = false;
    private bool facingRight = false;

    //References
    //Player GameObject MUST have both this controller script and the IsometricCharacterRenderer
    private IsometricCharacterRenderer isoRenderer;
    private Rigidbody2D rb;
    private GameManager manager;

    private Vector2 movement;

    private void Awake()
    {
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();
        GetComponent<PlayerAim>().OnShoot += IsometricPlayerController_OnShoot;

        //Set speed to base speed
        currentSpeed = baseMovementSpeed;
    }

    private void IsometricPlayerController_OnShoot(object sender, PlayerAim.OnShootEventArgs e)
    {
        Transform projectileTransform = Instantiate(projectilePrefab, e.gunEndPointPosition, Quaternion.identity);

        Vector3 shootDir = (e.shootPosition - e.gunEndPointPosition).normalized;
        projectileTransform.GetComponent<PlayerProjectile>().Setup(shootDir);
    }

    private void Update()
    {
        if(isActivePlayer)
        {
            //Increment time variables
            timeSinceDodge += Time.deltaTime;
            timeSinceSwing += Time.deltaTime;

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
                currentSpeed *= sprintMultiplier;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
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
        Type1[] enemies = FindObjectsOfType<Type1>();

        foreach (Type1 enemy in enemies)
        {
            //If the dummy is within swingRange, attack
            if (Vector2.Distance(transform.position, enemy.transform.position) <= swingRange)
            {
                //Check to make sure the dummy is on the correct side
                if (facingLeft == true && enemy.transform.position.x <= transform.position.x)
                {
                    enemy.TakeDamage(damage);
                }
                else if (facingRight == true && enemy.transform.position.x > transform.position.x)
                {
                    enemy.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("No enemy detected");
                }
            }
        }

        timeSinceSwing = 0f;
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
        if(!manager.astralPlane)
        {
            manager.astralPlane = true;
            manager.ToAstralPlane();
        }
        Destroy(gameObject);
    }
}
