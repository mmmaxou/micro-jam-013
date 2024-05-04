using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.Tilemaps;


public enum MudState
{
    Eat,
    Shit,
}


public class PlayerCollision : MonoBehaviour
{


    public Tile normalTile;
    public GameObject worm;
    public MudState mudState = MudState.Eat;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.mudState == MudState.Shit)
        {
            this.ShitMud();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("click click");
        this.ChangeMudState();
    }

    private void ChangeMudState()
    {
        if (this.mudState == MudState.Eat)
        {
            this.mudState = MudState.Shit;
            this.spriteRenderer.color = Color.cyan;
        }
        else if (this.mudState == MudState.Shit)
        {
            this.mudState = MudState.Eat;
            this.spriteRenderer.color = Color.red;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.mudState == MudState.Eat)
            EatMud(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.mudState == MudState.Eat)
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

    private void ShitMud()
    {

    }

}
