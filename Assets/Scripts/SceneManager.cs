using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    Menu,
    Main,
    GameOver
}

/// <summary>
/// Control the current scene of the game.
/// This class is a Monobehavior instanciated in the __app prefab in the __app scene that should always be loaded
/// Make sure your scene contains the prefab DevPreload to load it
/// </summary>
public class SceneManager : MonoBehaviourSingleton<SceneManager>
{

    public GameState state;

    /// <summary> Simply define our state and launch the menu </summary>
    void Start()
    {
        this.state = GameState.Menu;
        this.LaunchMenu();
    }

    /// ========================================
    /// PUBLIC METHODS
    /// ========================================


    /// <summary> Transition scene to menu and transition music </summary>
    public void LaunchMenu()
    {
        // MusicManager.Instance.TransitionToMenu();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        SceneManager.Instance.state = GameState.Menu;
    }

    /// <summary> Transition scene to main game and transition music </summary>
    public void LaunchMain()
    {
        // MusicManager.Instance.TransitionToMain();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        SceneManager.Instance.state = GameState.Main;
    }

    /// <summary> Transition scene to game over and transition music </summary>
    public void LaunchGameOver()
    {
        // MusicManager.Instance.TransitionToGameOver();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        SceneManager.Instance.state = GameState.GameOver;
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