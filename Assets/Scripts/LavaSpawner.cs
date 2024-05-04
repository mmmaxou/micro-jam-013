using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class LavaSpawner : MonoBehaviour
{
    public Object LavaParticle;
    public int FrameBeforeSpawn = 1500;
    public int FrameBetweenSpawn = 10;
    private int Frame = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Frame >= FrameBeforeSpawn && (Frame-FrameBetweenSpawn) % FrameBetweenSpawn == 0)
        {
            
            Object.Instantiate(LavaParticle, this.transform.position + Random.insideUnitSphere, this.transform.rotation, this.transform);

            Debug.Log("Spawned Lava");
        }

        Debug.Log("Frame");
        Frame ++;
    }
}
