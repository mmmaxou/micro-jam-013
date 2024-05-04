using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSquare : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      float horizontalInput = Input.GetAxis("Horizontal");
      transform.position = transform.position + new Vector3(horizontalInput * Time.deltaTime, 0, 0);
    }
}
