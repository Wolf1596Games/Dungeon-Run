using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] float baseMovementSpeed = 1f;
    [SerializeField] float speed = 0f;
    [SerializeField] float sprintMultiplier = 1.2f;
    [SerializeField] float dodgeDuration = .5f;
    [SerializeField] float dodgeCooldown = 5f;
    [SerializeField] bool sprinting = false;

    [Header("Player Health")]
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth = 3;

    [Header("Player Fighting")]
    [SerializeField] public int damage = 1;
    [SerializeField] float swingRange = 3f;
    [SerializeField] float timeBetweenSwings = .35f;
    [SerializeField] float timeBetweenShots = .35f;

    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 5f;

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
    public bool movingLeft = false;
    public bool movingRight = true;

    //References
    Rigidbody2D rb;
    BoxCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = FindObjectOfType<Rigidbody2D>();
        playerCollider = FindObjectOfType<BoxCollider2D>();

        //Set speed to base speed
        speed = baseMovementSpeed;
    }

    // Update is called once per frame
    void Update()
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
        if(rb.velocity.x < 0)
        {
            movingLeft = true;
            movingRight = false;
        }
        else if(rb.velocity.x > 0)
        {
            movingLeft = false;
            movingRight = true;
        }

        //Sprinting
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprinting = true;

            speed *= sprintMultiplier;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;

            speed = baseMovementSpeed;
        }

        //Horizontal Movement
        horizontalMovement = Input.GetAxis("Horizontal");

        if(horizontalMovement > 0f)
        {
            MoveHorizontal();

            if(Input.GetButtonDown("Dodge") && timeSinceDodge >= 5f)
            {
                StartCoroutine("DodgeCoroutine");
            }
        }
        else if(horizontalMovement < 0f)
        {
            MoveHorizontal();

            if (Input.GetButtonDown("Dodge") && timeSinceDodge >= 5f)
            {
                StartCoroutine("DodgeCoroutine");
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Vertical Movement
        verticalMovement = Input.GetAxis("Vertical");

        if(verticalMovement > 0f)
        {
            MoveVertical();

            if (Input.GetButtonDown("Dodge") && timeSinceDodge >= 5f)
            {
                StartCoroutine("DodgeCoroutine");
            }
        }
        else if(verticalMovement < 0f)
        {
            MoveVertical();

            if (Input.GetButtonDown("Dodge") && timeSinceDodge >= 5f)
            {
                StartCoroutine("DodgeCoroutine");
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        //Melee Attack
        if(Input.GetButtonDown("Fire1") && timeSinceSwing >= timeBetweenSwings)
        {
            MeleeAttack();
        }
        //Ranged Attack
        if(Input.GetButtonDown("Fire2") && timeSinceShot >= timeBetweenShots)
        {
            RangedAttack();
        }
    }

    //Basic Movement
    private void MoveHorizontal()
    {
        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
    }
    private void MoveVertical()
    {
        rb.velocity = new Vector2(rb.velocity.x, verticalMovement * speed);
    }
    //Dodging
    private IEnumerator DodgeCoroutine()
    {
        speed *= 2.5f;
        timeSinceDodge = 0f;

        yield return new WaitForSeconds(dodgeDuration);

        speed /= 2.5f;
    }

    //Attacking
    public void MeleeAttack()
    {
        TestDummy[] dummies = FindObjectsOfType<TestDummy>();

        foreach(TestDummy dummy in dummies)
        {
            if(Vector2.Distance(transform.position, dummy.transform.position) <= swingRange)
            {
                dummy.TakeDamage(damage);
            }
        }

        timeSinceSwing = 0f;
    }
    public void RangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = Vector2.right * projectileSpeed;

        timeSinceShot = 0f;
    }

    //Taking damage
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
