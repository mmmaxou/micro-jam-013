using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{
    public int NbRock = 0;
    public int NbMaxRock = 100;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
