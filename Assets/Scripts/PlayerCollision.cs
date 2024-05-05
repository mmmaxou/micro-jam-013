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
    Sleep,
    Repeat,
}


public class PlayerCollision : MonoBehaviour
{
    private MudState mudState = MudState.Sleep;
    private SpriteRenderer spriteRenderer;
    private TileModifier tileModifier;
    private Snake snake;
    private int mudPerSnakePart = 20;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.tileModifier = GameObject.FindGameObjectsWithTag("GridTop").First().GetComponent<TileModifier>();
        this.snake = transform.parent.GetComponent<Snake>();

        this.SetMudStateEat();
        if (this.tileModifier == null)
        {
            Debug.LogException(new NullReferenceException("TileModifier: grid is null"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.mudState == MudState.Eat)
                this.SetMudStateSleep();
            else
                this.SetMudStateEat();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (this.mudState == MudState.Shit)
                this.SetMudStateSleep();
            else
                this.SetMudStateShit();
        }

        if (this.mudState == MudState.Shit)
        {
            this.ShitMud();
        }
    }

    private void SetMudStateSleep()
    {
        this.mudState = MudState.Sleep;
        this.spriteRenderer.color = Color.green;
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

        int currentMudPerSnakePart = 1 + (GameManager.Instance.NbRock / mudPerSnakePart);
        if (currentMudPerSnakePart > snake.snakeBody.Count)
        {
            snake.CreateBodyPart();
        }
    }

    private void ShitMud()
    {
        if (GameManager.Instance.NbRock > 0)
        {
            this.tileModifier.PlaceDirt(this.gameObject.transform.position, this.gameObject.transform.localScale.x);
        }

        int currentMudPerSnakePart = 1 + (GameManager.Instance.NbRock / mudPerSnakePart);
        if (currentMudPerSnakePart < snake.snakeBody.Count)
        {
            snake.RemoveBodyPart();
        }
    }

}
