using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCollision : MonoBehaviour
{


    public Tile normalTile;
    public GameObject worm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="collision">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contactCount != 1)
        {
            Debug.Log("more than one collision");
            return;
        }

        // change the collided tile on the tilemap
        collision.gameObject.GetComponent<TileModifier>().ChangeTile(collision.contacts[0]);
    }
}
