using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] float baseMovementSpeed = 1f;
    [SerializeField] float speed = 0f;
    [SerializeField] float sprintMultiplier = 1.2f;
    [SerializeField] bool sprinting = false;


    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;


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
        }
        else if(horizontalMovement < 0f)
        {
            MoveHorizontal();
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
        }
        else if(verticalMovement < 0f)
        {
            MoveVertical();
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void MoveHorizontal()
    {
        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
    }
    private void MoveVertical()
    {
        rb.velocity = new Vector2(rb.velocity.x, verticalMovement * speed);
    }
}
