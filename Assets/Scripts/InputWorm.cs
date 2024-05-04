using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputWorm : MonoBehaviour
{
  private AudioSource audioSource;
  //private bool hasBeenReleased = true;
  //private float lastMoveTime = 0;
  //private float MOVE_COOL_DOWN = 0.3f;
  private float MOVE_SPEED = 6f;

  // Start is called before the first frame update
  void Start()
    {
      audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    //if (hasBeenReleased)
    //{
    //  float moveHorizontal = Input.GetAxisRaw("Horizontal");
    //  if (moveHorizontal != 0)
    //  {
    //    // Moving.
    //    transform.position = transform.position + new Vector3(moveHorizontal, 0, 0);
    //    audioSource.Play();
    //    lastMoveTime = Time.time;
    //    hasBeenReleased = false;
    //  }
    //}

    //// Reset input if buttons are released.
    //if (/*Input.GetAxisRaw("Horizontal") == 0 || */(Time.time - lastMoveTime) > MOVE_COOL_DOWN)
    //{
    //  hasBeenReleased = true;
    //}
    float moveHorizontal = Input.GetAxisRaw("Horizontal");
    transform.position = transform.position + new Vector3(moveHorizontal * Time.deltaTime * MOVE_SPEED, 0, 0);
    if (Input.GetAxisRaw("Horizontal") != 0)
    {
      if (!audioSource.isPlaying)
      {
        audioSource.Play();
      }
    } else
    {
      audioSource.Stop();
    }
  }
}
