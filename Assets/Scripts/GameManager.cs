using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public int NbRock = 0;
    public int MaxNbRock = 100;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1080, 1920, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
