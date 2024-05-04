using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputWorm : MonoBehaviour
{
  private AudioSource audioSource;
  private bool hasBeenReleased = true;

  // Start is called before the first frame update
  void Start()
    {
      audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if (hasBeenReleased)
      {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (moveHorizontal != 0)
        {
          // Moving.
          transform.position = transform.position + new Vector3(moveHorizontal, 0, 0);
          audioSource.Play();
          hasBeenReleased = false;
        }
      }

      // Reset input if buttons are released.
      if (Input.GetAxisRaw("Horizontal") == 0)
      {
        hasBeenReleased = true;
      }
    }
}
