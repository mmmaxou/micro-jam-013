using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;
using System.Collections.Generic;

public class TileModifier : MonoBehaviour
{
    public Tile dugTile;
    public RuleTile fullTile;
    private Tilemap grid;
    public HashSet<Vector3Int> tilesToPlace;
    public HashSet<Vector3Int> currentColliders;

    // Start is called before the first frame update
    void Start()
    {

        this.tilesToPlace = new HashSet<Vector3Int>();
        this.currentColliders = new HashSet<Vector3Int>();

        grid = GetComponent<Tilemap>();
        if (grid == null)
        {
            Debug.LogException(new NullReferenceException("TileModifier: grid is null"));
        }
    }

    // Update is called once per frame
    void Update()
    {

        HashSet<Vector3Int> toRemove = new HashSet<Vector3Int>();

        foreach (Vector3Int cellPosition in this.tilesToPlace)
        {
            if (!this.currentColliders.Contains(cellPosition))
            {
                GameManager.Instance.NbRock--;
                grid.SetTile(cellPosition, fullTile);
                toRemove.Add(cellPosition);
            }
        }

        foreach (Vector3Int cellPosition in toRemove)
        {
            this.tilesToPlace.Remove(cellPosition);
            this.currentColliders.Remove(cellPosition);
        }
    }

    public void DigHole(Vector3 collision)
    {
        grid.SetTile(grid.WorldToCell(collision), dugTile);
        GameManager.Instance.NbRock++;
    }

    public void PlaceDirt(Vector3 position, float radius)
    {
        Vector2 point = new Vector2(position.x, position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius);
        currentColliders.Clear();
        foreach (Collider2D collider in colliders)
        {
            Vector3Int cellPosition = grid.WorldToCell(collider.transform.position);
            currentColliders.Add(cellPosition);

            if (!grid.HasTile(cellPosition))
                tilesToPlace.Add(cellPosition);
        }

    }
}
