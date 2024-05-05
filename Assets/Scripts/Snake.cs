using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class Snake : MonoBehaviour
{
    [SerializeField]
    private GameObject bodyPart;

    [SerializeField]
    private List<GameObject> snakeBody = new List<GameObject>(1);

    [SerializeField]
    private float distanceBetween = 0.8f;

    [SerializeField]
    [Range(0.01f, 0.99f)]
    private float cohesion = 0.7f;

    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        snakeBody.Add(transform.GetChild(0).gameObject);
    }

    private void FixedUpdate()
    {
        SnakeMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            CreateBodyPart();
            Debug.Log("+");
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            Debug.Log("-");
    }


    void SnakeMovement()
    {
        if (snakeBody.Count > 1)
        {
            for (int snakeIndex = 1; snakeIndex < snakeBody.Count; snakeIndex++)
            {
                MarkerManager mm = snakeBody[snakeIndex - 1].GetComponent<MarkerManager>();
                MarkerManager.Marker marker = mm.GetFirstMarkerAtDistance(snakeBody[snakeIndex - 1].transform.position, distanceBetween);
                if (marker != null)
                {
                    position = Vector2.Lerp(snakeBody[snakeIndex].transform.position, marker.position, cohesion);
                    snakeBody[snakeIndex].GetComponent<Rigidbody2D>().MovePosition(position);
                    snakeBody[snakeIndex].transform.rotation = marker.rotation;
                }
            }
        }
    }

    private void CreateBodyPart()
    {
        MarkerManager mm = snakeBody[snakeBody.Count - 1].GetComponent<MarkerManager>();
        MarkerManager.Marker marker = mm.GetFirstMarkerAtDistance(snakeBody[snakeBody.Count - 1].transform.position, distanceBetween);
        GameObject temp;
        if (marker != null)
        {
            temp = Instantiate(bodyPart, marker.position, marker.rotation, transform);
        }
        else
        {
            temp = Instantiate(bodyPart, snakeBody[snakeBody.Count - 1].transform.position, snakeBody[snakeBody.Count - 1].transform.rotation, transform);
        }
        snakeBody.Add(temp);
        // temp.GetComponent<MarkerManager>().ClearMarkerList();
    }
}
