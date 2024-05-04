using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPreload : MonoBehaviour
{
    /// <summary>
    /// Class created based on this article : https://stackoverflow.com/questions/35890932/unity-game-manager-script-works-only-one-time
    /// Auto load the __app scene
    /// </summary>
    void Awake()
    {
        GameObject check = GameObject.Find("__app");
        if (check == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("_preload");
        }
    }
}
