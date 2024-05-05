using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public float maxSpeed = 0.75f;
    Rigidbody2D rigidBody;
    Vector2 nextPosition = new Vector2(0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nextPosition = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        nextPosition = Vector2.MoveTowards(transform.position, nextPosition, maxSpeed);
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(nextPosition);
        rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);
    }
}
