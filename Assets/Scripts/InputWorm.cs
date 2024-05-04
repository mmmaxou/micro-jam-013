using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InputWorm : MonoBehaviour
{
  private AudioSource audioSource;
  private bool hasBeenReleased = true;
  private float lastMoveTime = 0;
  private float MOVE_COOL_DOWN = 0.3f;

  // Start is called before the first frame update
  void Start()
  {
    audioSource = GetComponent<AudioSource>();
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

  // // Update is called once per frame
  // void Update()
  // {
  //   if (hasBeenReleased)
  //   {
  //     float moveHorizontal = Input.GetAxisRaw("Horizontal");
  //     if (moveHorizontal != 0)
  //     {
  //       // Moving.
  //       transform.position = transform.position + new Vector3(moveHorizontal, 0, 0);

  //       if (audioSource)
  //         audioSource.Play();

  //       lastMoveTime = Time.time;
  //       hasBeenReleased = false;
  //     }
  //   }

  //   // Reset input if buttons are released.
  //   if (/*Input.GetAxisRaw("Horizontal") == 0 || */(Time.time - lastMoveTime) > MOVE_COOL_DOWN)
  //   {
  //     hasBeenReleased = true;
  //   }
  // }
}
