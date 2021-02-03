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
    [SerializeField] int damage = 1;
    [SerializeField] float swingRange = 3f;
    [SerializeField] float timeBetweenSwings = .35f;
    [SerializeField] float timeBetweenShots = .35f;
    [SerializeField] GameObject projectilePrefab;

    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;
    private float timeSinceDodge = 5f;
    private float timeSinceSwing = .35f;
    private float timeSinceShot = .35f;

    Rigidbody2D rb;
    BoxCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = FindObjectOfType<Rigidbody2D>();
        playerCollider = FindObjectOfType<BoxCollider2D>();

        speed = baseMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceDodge += Time.deltaTime;

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

        if(Input.GetButtonDown("Fire1") && timeSinceSwing >= timeBetweenSwings)
        {
            MeleeAttack();
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
    }
    public void RangedAttack()
    {

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
