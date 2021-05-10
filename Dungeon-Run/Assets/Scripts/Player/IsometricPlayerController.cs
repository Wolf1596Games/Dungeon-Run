using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsometricPlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [Tooltip("Base player movement speed")]
    public float baseMovementSpeed = 2f;
    [Tooltip("Current player movement speed")]
    public float currentSpeed = 0f;
    [Tooltip("Multiplier for sprinting")]
    public float sprintMultiplier = 1.75f;
    [Tooltip("Duration of the player's dodge")]
    public float dodgeDuration = .5f;
    [Tooltip("Player's dodge cooldown")]
    public float dodgeCooldown = 5f;
    [Tooltip("Multiplier for dodging")]
    public float dodgeMultiplier = 2.5f;

    [Header("Player Health")]
    [Tooltip("Player's max health")]
    public int maxHealth = 6;
    [Tooltip("Player's current health")]
    public int currentHealth = 6;
    [Tooltip("Slider Health Display")]
    public Slider healthSlider;
    [Tooltip("Cooldown for player getting damaged")]
    public float damageCooldown = .75f;
    [Tooltip("Time since last damaged")]
    public float timeSinceDamaged;

    [Header("Player Combat")]
    [Tooltip("Player's damage per hit")]
    public int damage = 1;
    [Tooltip("Player's melee swing range")]
    public float swingRange = 1.5f;
    [Tooltip("Time between player's melee attacks")]
    public float timeBetweenSwings = .35f;

    [Header("Projectile")]
    [Tooltip("Prefab object for projectiles")]
    public Transform projectilePrefab;

    [Header("Audio")]
    [Tooltip("Array of sounds for when the player is damaged")]
    public AudioClip[] damagedSounds;
    [Tooltip("Array of sounds for when the player fires a projectile")]
    public AudioClip[] projectileSounds;
    [Tooltip("Array of sounds for when the player uses a melee attack")]
    public AudioClip[] meleeSounds;

    [Tooltip("Whether or not this player object is the active player")]
    public bool isActivePlayer = false;

    //Private movement variables
    private float timeSinceDodge = 5f;

    //Private fighting variables
    private float timeSinceSwing = .35f;
    private bool facingLeft = false;
    private bool facingRight = false;

    [Header("References")]
    public Animator anim;
    //References
    //Player GameObject MUST have both this controller script and the IsometricCharacterRenderer
    private IsometricCharacterRenderer isoRenderer;
    private Rigidbody2D rb;
    private GameManager manager;
    private AudioSource audioSource;

    
    public Vector2 movement;

    private void Awake()
    {
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<PlayerAim>().OnShoot += IsometricPlayerController_OnShoot;

        if(healthSlider == null)
        {
            healthSlider = FindObjectOfType<Slider>();
        }

        //Set speed to base speed
        currentSpeed = baseMovementSpeed;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        timeSinceDodge = dodgeCooldown;
        timeSinceSwing = timeBetweenSwings;
    }

    private void IsometricPlayerController_OnShoot(object sender, PlayerAim.OnShootEventArgs e)
    {
        audioSource.PlayOneShot(projectileSounds[Random.Range(0, projectileSounds.Length)]);
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
            timeSinceDamaged += Time.deltaTime;

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
                //MeleeAttack();
                StartCoroutine("Melee");
            }
        }
    }

    private void FixedUpdate()
    {
        if (isActivePlayer)
        {
            if(movement.magnitude >= 0.1f && anim.GetBool("attacking") == false)
            {
                anim.SetBool("moving", true);
                anim.SetBool("attacking", false);                
            }
            else if(movement.magnitude <= 0f && anim.GetBool("attacking") == false)
            {
                anim.SetBool("moving", false);
            }
            //Movement
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
            //isoRenderer.SetDirection(movement);
            Animate();
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
    private IEnumerator Melee()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        audioSource.PlayOneShot(meleeSounds[Random.Range(0, meleeSounds.Length)]);
        anim.SetBool("moving", false);
        anim.SetBool("attacking", true);
        foreach (Enemy enemy in enemies)
        {
            //If the dummy is within swingRange, attack
            if (Vector2.Distance(transform.position, enemy.transform.position) <= swingRange)
            {
                //Check to make sure the dummy is on the correct side
                if (facingLeft == true && enemy.transform.position.x <= transform.position.x)
                {
                    //audioSource.PlayOneShot(meleeSounds[Random.Range(0, meleeSounds.Length)]);
                    enemy.TakeDamage(damage);
                }
                else if (facingRight == true && enemy.transform.position.x > transform.position.x)
                {
                    //audioSource.PlayOneShot(meleeSounds[Random.Range(0, meleeSounds.Length)]);
                    enemy.TakeDamage(damage);
                }
                else
                {

                    Debug.Log("No enemy detected");
                }
            }
        }

        yield return new WaitForSeconds(timeBetweenSwings);
        anim.SetBool("attacking", false);
        timeSinceSwing = 0f;
    }

    //Taking damage
    public void TakeDamage(int damageTaken)
    {
        if(timeSinceDamaged >= damageCooldown)
        {
            currentHealth -= damageTaken;

            healthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                Death();
            }

            timeSinceDamaged = 0f;
        }
    }
    public void Death()
    {
        manager = FindObjectOfType<GameManager>();
        if (!manager.astralPlane)
        {
            manager.ToAstralPlane();
        }
        else
        {
            manager.GameOver();
        }
        //Destroy(gameObject);
    }

    private void Animate()
    {
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", currentSpeed);
    }
}
