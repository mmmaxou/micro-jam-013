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
        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount]; ;
        collision.GetContacts(contacts);

        foreach (ContactPoint2D contact in contacts)
        {
            Vector3 coordinates = contact.point;
            collision.gameObject.GetComponent<TileModifier>().DigHole(coordinates);
        }

    }
}
