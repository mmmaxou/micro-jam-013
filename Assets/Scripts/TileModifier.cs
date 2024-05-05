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
    private Grid Grid;
    private Tilemap DirtTilemap;
    private Tilemap BasaltTilemap;
    public HashSet<Vector3Int> tilesToPlace;
    public HashSet<Vector3Int> currentColliders;

    // Start is called before the first frame update
    void Start()
    {

        this.tilesToPlace = new HashSet<Vector3Int>();
        this.currentColliders = new HashSet<Vector3Int>();

        Grid = this.gameObject.transform.parent.gameObject.GetComponent<Grid>();
        if (Grid == null)
        {
            Debug.LogException(new NullReferenceException("TileModifier: Grid is null"));
        }

        BasaltTilemap = Grid.transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        if (BasaltTilemap == null)
        {
            Debug.LogException(new NullReferenceException("TileModifier: BasaltTilemap is null"));
        }

        DirtTilemap = Grid.transform.GetChild(1).gameObject.GetComponent<Tilemap>();
        if (DirtTilemap == null)
        {
            Debug.LogException(new NullReferenceException("TileModifier: DirtTilemap is null"));
        }
    }

    // Update is called once per frame
    void Update()
    {

        HashSet<Vector3Int> toRemove = new HashSet<Vector3Int>(16);

        foreach (Vector3Int cellPosition in this.tilesToPlace)
        {
            if (!this.currentColliders.Contains(cellPosition))
            {
                GameManager.Instance.NbRock--;
                DirtTilemap.SetTile(cellPosition, fullTile);
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
        DirtTilemap.SetTile(DirtTilemap.WorldToCell(collision), dugTile);
        GameManager.Instance.NbRock++;
    }

    public void PlaceDirt(Vector3 position, float radius)
    {
        Vector2 point = new Vector2(position.x, position.y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius);
        currentColliders.Clear();
        foreach (Collider2D collider in colliders)
        {
            Vector3Int cellPosition = Grid.WorldToCell(collider.transform.position);
            currentColliders.Add(cellPosition);

            bool isDirt = DirtTilemap.HasTile(cellPosition);
            bool isBasalt = BasaltTilemap.HasTile(cellPosition);

            if (!isDirt && !isBasalt)
                tilesToPlace.Add(cellPosition);
        }
    }
}
