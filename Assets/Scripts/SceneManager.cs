using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    Menu,
    Main,
    GameOver,
    GameEnding
}

public class SceneManager : MonoBehaviourSingleton<SceneManager>
{

    public GameState state;

    /// <summary> Simply define our state and launch the menu </summary>
    void Start()
    {
        this.state = GameState.Menu;
        this.LaunchMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W key pressed");
            LaunchGameEnding();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L key pressed");
            LaunchGameOver();
        }
    }


    public void LaunchMenu()
    {
        MusicManager.Instance.TransitionToMenu();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        SceneManager.Instance.state = GameState.Menu;
    }

    public void LaunchWipLevel()
    {
        MusicManager.Instance.TransitionToMain();
        UnityEngine.SceneManagement.SceneManager.LoadScene("WipLevel");
        SceneManager.Instance.state = GameState.Main;
        GameManager.Instance.NbRock = 0;
    }

    public void LaunchLevel01()
    {
        MusicManager.Instance.TransitionToMain();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level01");
        SceneManager.Instance.state = GameState.Main;
        GameManager.Instance.NbRock = 0;
    }

    public void LaunchLevel02()
    {
        MusicManager.Instance.TransitionToMain();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level02");
        SceneManager.Instance.state = GameState.Main;
        GameManager.Instance.NbRock = 0;
    }

    public void LaunchGameOver()
    {
        MusicManager.Instance.TransitionToGameOver();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        SceneManager.Instance.state = GameState.GameOver;
    }

    public void LaunchGameEnding()
    {
        MusicManager.Instance.TransitionToGameEnding();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameEnding");
        SceneManager.Instance.state = GameState.GameEnding;
    }

    /// <summary> Quit the game abruptly </summary>
    public void Quit()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}