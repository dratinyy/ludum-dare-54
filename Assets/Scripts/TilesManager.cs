using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    public GameObject tilePrefab;

    public int width = 7;
    public float tileWidth = 4;

    private List<GameObject> Tiles;

    // Start is called before the first frame update
    void Start()
    {
        // initialize Tiles 
        Tiles = new List<GameObject>(width * width);
        // create tiles
        for(int i = 0; i < width; i++)
        {
          for(int j = 0; j < width; j++)
          {
            // create tile
            GameObject tile = Instantiate(tilePrefab);
            tile.transform.position = new Vector3(i * tileWidth, j * tileWidth, 0);
            tile.transform.parent = transform;
            tile.transform.localScale = new Vector3(tileWidth, tileWidth , 1);
            Tiles.Add(tile);

            // in the corners of the map, there are no tiles
            if(noTiles(i, j))
            {
              tile.GetComponent<SpriteRenderer>().color = Color.black;
              tile.SetActive(false);
            }
          }
        }

        // center tile is walkable
        Tiles[getTile(width / 2, width / 2)].GetComponent<Tile>().setIsWalkable(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getTile(int x, int y)
    {
      if(noTiles(x, y))
      {
        return -1;
      }
      if(x < 0 || x >= width || y < 0 || y >= width)
      {
        return -1;
      }
      return x * width + y;
    }

    private bool noTiles(int x, int y)
    {
      // no corner tiles
      if(x == 0 && y == 0)
      {
        return true;
      }
      if(x == 0 && y == width - 1)
      {
        return true;
      }
      if(x == width - 1 && y == 0)
      {
        return true;
      }
      if(x == width - 1 && y == width - 1)
      {
        return true;
      }
      return false;
    }
}
