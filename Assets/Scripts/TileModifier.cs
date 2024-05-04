using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TileModifier : MonoBehaviour
{
    private Tile dugTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void DigHole(Vector3 collision)
    {
        Tilemap grid = GetComponent<Tilemap>();
        if (grid == null)
        {
            Debug.LogException(new NullReferenceException("TileModifier: grid is null"));
        }
        grid.SetTile(grid.WorldToCell(collision), dugTile);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
