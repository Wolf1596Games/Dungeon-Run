﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    GameManager manager;
    Scene scene;

    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadStart()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void LoadControls()
    {
        SceneManager.LoadScene("ControlsScene");
    }
    public void StartGame()
    {
        manager = FindObjectOfType<GameManager>();
        //StartCoroutine("StartGameCoroutine");
        manager.StartGame();
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Restart()
    {
        scene = SceneManager.GetActiveScene();
        int currentSceneIndex = scene.buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
