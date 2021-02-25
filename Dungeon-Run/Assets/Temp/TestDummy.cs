using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour
{
    [SerializeField] int health = 3;

    RoomManager[] rooms;
    public RoomManager activeRoom;

    private void Start()
    {
        //Determines which room the dummy (enemy) is in
        rooms = FindObjectsOfType<RoomManager>();

        foreach(RoomManager room in rooms)
        {
            if(Vector2.Distance(transform.position, room.transform.position) < 10)
            {
                activeRoom = room;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            activeRoom.EnemyKilled();

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(FindObjectOfType<GameManager>().astralPlane == false)
            {
                Debug.Log("Attacked player");

                collision.gameObject.GetComponent<IsometricPlayerController>().TakeDamage(1);
            }
            else
            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }
    }
}
