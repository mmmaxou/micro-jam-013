using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private MudState mudState = MudState.Eat;
    private SpriteRenderer spriteRenderer;
    private TileModifier tileModifier;
    private CircleCollider2D circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.tileModifier = GameObject.FindGameObjectsWithTag("GridTop").First().GetComponent<TileModifier>();
        this.circleCollider2D = GetComponent<CircleCollider2D>();

        this.SetMudStateEat();
        if (this.tileModifier == null)
        {
            Debug.LogException(new NullReferenceException("TileModifier: grid is null"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.mudState == MudState.Shit)
        {
            this.ShitMud();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left-click.");
            this.ChangeMudState();
        }
    }

    private void ChangeMudState()
    {
        if (this.mudState == MudState.Eat)
            SetMudStateShit();
        else if (this.mudState == MudState.Shit)
            SetMudStateEat();
    }

    private void SetMudStateShit()
    {
        this.mudState = MudState.Shit;
        this.spriteRenderer.color = Color.cyan;
        // this.circleCollider2D.enabled = false;
    }
    private void SetMudStateEat()
    {
        this.mudState = MudState.Eat;
        this.spriteRenderer.color = Color.red;
        // this.circleCollider2D.enabled = true;
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
            if (GameManager.Instance.NbRock < GameManager.Instance.MaxNbRock)
            {
                Vector3 coordinates = contact.point;
                Vector3 direction = coordinates - this.gameObject.transform.position;
                Vector3 scaledDirection = direction * 0.1f;
                collision.gameObject.GetComponent<TileModifier>()?.DigHole(coordinates + scaledDirection);
            }
            else
                break;
        }
    }

    private void ShitMud()
    {
        if (GameManager.Instance.NbRock > 0)
        {
            this.tileModifier.PlaceDirt(this.gameObject.transform.position, this.gameObject.transform.localScale.x);
        }
    }

}
