using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CountTiles : MonoBehaviour
{
    public Tilemap tilemap;
    public List<Vector3> tileWorldLocations;
    public LevelObj[] TileObjects;
    // Use this for initialization
    void Start()
    {
        tileWorldLocations = new List<Vector3>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            if (tilemap.HasTile(localPlace))
            {
                tileWorldLocations.Add(localPlace);

            }

        }
    }



}
