using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Plane Management")]
    [Tooltip("Controls which \"plane\" the player is in.")]
    public bool astralPlane = false;

    public IsometricPlayerController[] players;
    public IsometricPlayerController activePlayer;

    private CameraFollow mainCam;

    private void Awake()
    {
        players = GetPlayers();
        mainCam = FindObjectOfType<CameraFollow>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int numberGameManagers = FindObjectsOfType<GameManager>().Length;
        if(numberGameManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        ChooseActivePlayer();
    }

    public void ToAstralPlane()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public IsometricPlayerController[] GetPlayers()
    {
        return FindObjectsOfType<IsometricPlayerController>();
    }

    public void ChooseActivePlayer()
    {
        activePlayer = players[Random.Range(0, players.Length)];

        activePlayer.isActivePlayer = true;

        mainCam.target = activePlayer.transform;

        foreach(IsometricPlayerController player in players)
        {
            if(!player.isActivePlayer)
            {
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }
}
