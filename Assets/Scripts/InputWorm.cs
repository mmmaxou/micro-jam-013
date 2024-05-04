using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InputWorm : MonoBehaviour
{

  // Start is called before the first frame update
  void Start()
  {
  }


  private void Update()
  {
    if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
    {
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      worldPosition.z = 0.0f;
      transform.position = worldPosition;
    }
  }
}
