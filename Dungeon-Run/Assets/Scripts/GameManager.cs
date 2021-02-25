using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Plane Management")]
    [Tooltip("Controls which \"plane\" the player is in.")]
    public bool astralPlane = false;

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
    }

    public void ToAstralPlane()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
