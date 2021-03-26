using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Plane Management")]
    [Tooltip("Controls which \"plane\" the player is in.")]
    public bool astralPlane = false;
    [Tooltip("Name of the scene the player should be transferred to upon death")]
    [SerializeField] private string astralPlaneScene;

    public IsometricPlayerController[] players;
    public IsometricPlayerController activePlayer;
    public int currentSceneIndex;
    public int lastSceneIndex;

    Scene scene;

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

        if(players.Length != 0)
        {
            ChooseActivePlayer();
        }
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        currentSceneIndex = scene.buildIndex;
    }

    public void StartGame()
    {
        StartCoroutine("StartGameCoroutine");
    }
    public void ToAstralPlane()
    {
        StartCoroutine("AstralPlaneCoroutine");
    }
    public void FromAstralPlane()
    {
        StartCoroutine("FromAstralPlaneCoroutine");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void ToNextLevel()
    {
        StartCoroutine("NextLevelCoroutine");
    }

    private IEnumerator StartGameCoroutine()
    {
        SceneManager.LoadScene("Level One");

        yield return new WaitForSeconds(.1f);

        players = GetPlayers();
        ChooseActivePlayer();
    }
    private IEnumerator AstralPlaneCoroutine()
    {
        //yield return new WaitForSeconds(1.5f);

        lastSceneIndex = currentSceneIndex;
        SceneManager.LoadScene(astralPlaneScene);

        yield return new WaitForSeconds(.1f);

        players = GetPlayers();
        ChooseActivePlayer();
    }
    private IEnumerator FromAstralPlaneCoroutine()
    {
        SceneManager.LoadScene(lastSceneIndex);

        yield return new WaitForSeconds(.1f);

        players = GetPlayers();
        ChooseActivePlayer();
    }
    private IEnumerator NextLevelCoroutine()
    {
        lastSceneIndex = currentSceneIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

        yield return new WaitForSeconds(.1f);

        players = GetPlayers();

        ChooseActivePlayer();
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
