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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EatMud(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EatMud(collision);
    }

    private void EatMud(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        collision.GetContacts(contacts);
        foreach (ContactPoint2D contact in contacts)
        {
            Vector3 coordinates = contact.point;
            Vector3 direction = coordinates - this.gameObject.transform.position;
            Vector3 scaledDirection = direction * 0.1f;
            collision.gameObject.GetComponent<TileModifier>()?.DigHole(coordinates + scaledDirection);
        }
    }

}
