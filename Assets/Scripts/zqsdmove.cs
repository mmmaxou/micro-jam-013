using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zqsdmove : MonoBehaviour
{

    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        input = input.normalized;
        this.transform.position += input * speed * Time.deltaTime;
    }
}
