using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class LavaCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "House")
        {
            Debug.Log("collide with house");
            SceneManager.Instance.LaunchGameOver();
        }
        if (collision.gameObject.name == "Forge")
        {
            Debug.Log("collide with forge");
        }
    }

}
