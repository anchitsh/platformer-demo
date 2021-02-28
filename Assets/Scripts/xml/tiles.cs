﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class tiles : MonoBehaviour
{
    public Tile water;
    public Tile land;
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                Vector3Int p = new Vector3Int(x, y, 0);
                bool odd = (x + y) % 2 == 1;
                Tile tile = odd ? water : land;
                tilemap.SetTile(p, tile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
