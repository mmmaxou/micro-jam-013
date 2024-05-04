using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRelease : MonoBehaviour
{
  private float creationTime = 0;
  private float TIME_TO_LIVE = 2;

    // Start is called before the first frame update
    void Start()
    {
        creationTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - creationTime > TIME_TO_LIVE)
        {
            Destroy(gameObject);
        }
    }
}
