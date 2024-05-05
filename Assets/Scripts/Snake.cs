using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject bodyPart;

    [SerializeField]
    public List<GameObject> snakeBody = new List<GameObject>(1);

    [SerializeField]
    private float distanceBetween = 0.8f;

    [SerializeField]
    [Range(0.01f, 0.99f)]
    private float cohesion = 0.7f;

    private Vector2 position;

    private Sprite wormBody;

    private Sprite wormTail;

    // Start is called before the first frame update
    void Start()
    {
        snakeBody.Add(transform.GetChild(0).gameObject);
        Sprite[] sprites = Resources.LoadAll<Sprite>("SpriteVerChange");
        wormBody = sprites[0];
        wormTail = sprites[1];
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

    public void CreateBodyPart()
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
        temp.GetComponent<SpriteRenderer>().sortingOrder = 25 - snakeBody.Count;
        snakeBody.Add(temp);

        ChangeWormSprites();
    }

    public void RemoveBodyPart()
    {
        Object.Destroy(snakeBody[^1]);
        snakeBody.RemoveAt(snakeBody.Count - 1);
        ChangeWormSprites();
    }

    private void ChangeWormSprites()
    {
        if (snakeBody.Count > 2)
        {
            snakeBody[snakeBody.Count - 2].GetComponent<SpriteRenderer>().sprite = wormBody;
        }
        snakeBody[snakeBody.Count - 1].GetComponent<SpriteRenderer>().sprite = wormTail;
    }
}
