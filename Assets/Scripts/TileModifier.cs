using UnityEngine;
using UnityEngine.Tilemaps;

public class TileModifier : MonoBehaviour
{
    private int timestamp = 0;
    public Tile t1;
    public Tile t2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Tilemap grid = GetComponent<Tilemap>();

        {  // edit tiles
            
            Vector3Int myVector = new Vector3Int(1, 1, 0);
            if (timestamp == 60)
            {
                Debug.Log("set t2");
                grid.SetTile(myVector, t2);
            }
            else if (timestamp == 120)
            {
                Debug.Log("set t1");
                grid.SetTile(myVector, t1);
            }
            else if (timestamp == 121)
            {
                Debug.Log("reset");
                timestamp = 0;
            }
            
            timestamp++;
        }
        {  // On click event
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    
                var tpos = grid.WorldToCell(worldPoint);

                // Try to get a tile from cell position
                grid.SetTile(tpos, t1);
            }
        }
    }
}
