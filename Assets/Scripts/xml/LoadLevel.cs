using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadLevel : MonoBehaviour
{
    public Tile normal;
    public Tile stone;
    public Tilemap tilemapNormal, tilemapStone;
    public GameObject coinPrefab;
    void Start()
    {
        var tileCollection = ReadXML.Load(Path.Combine(Application.dataPath, "LevelData.xml"));

        print(tileCollection.TileObjects[0].type);


        for (int i = 0; i < tileCollection.TileObjects.Length; i++)
        {
            if (tileCollection.TileObjects[i].type == ObjType.Normal)
            {
                Vector3Int p = new Vector3Int((int)tileCollection.TileObjects[i].x, (int)tileCollection.TileObjects[i].y, 0);
                Tile tile = normal;
                tilemapNormal.SetTile(p, tile);
            }
            else if (tileCollection.TileObjects[i].type == ObjType.Stone)
            {
                Vector3Int p = new Vector3Int((int)tileCollection.TileObjects[i].x, (int)tileCollection.TileObjects[i].y, 0);
                Tile tile = stone;
                tilemapStone.SetTile(p, tile);
            }
            else if (tileCollection.TileObjects[i].type == ObjType.Coin)
            {
                GameObject coin = Instantiate(coinPrefab);
                coin.transform.position = new Vector3(tileCollection.TileObjects[i].x, tileCollection.TileObjects[i].y, 0);
            }

        }
    }

}
