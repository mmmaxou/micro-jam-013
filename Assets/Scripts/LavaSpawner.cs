using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class LavaSpawner : MonoBehaviour
{
    public Object LavaParticle;
    public float SecondsBeforeSpawn = 3.0f;
    public float SecondsBetweenSpawn = 0.05f;
    public int NbLavaParticle = 500;
    private float NextUpdateTime = .0f;
    private float TimePassed = .0f;
    // Start is called before the first frame update
    void Start()
    {
        NextUpdateTime = SecondsBeforeSpawn;
    }

    void FixedUpdate()
    {
        while (TimePassed >= NextUpdateTime && NbLavaParticle-- > 0)
        {
            Object.Instantiate(LavaParticle, this.transform.position + Random.insideUnitSphere, this.transform.rotation, this.transform);
            NextUpdateTime = TimePassed + SecondsBetweenSpawn;  // set next spawning time
            Debug.Log("Summon");
        }

        TimePassed += Time.fixedDeltaTime;
    }
}
