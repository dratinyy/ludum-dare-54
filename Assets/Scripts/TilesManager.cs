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
    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < width; j++)
      {
        // create tile
        GameObject tile = Instantiate(tilePrefab, new Vector3(i * tileWidth - (width / 2 * tileWidth), j * tileWidth - (width / 2 * tileWidth), 1), Quaternion.identity, transform);
        Tiles.Add(tile);

        // set menu not owned
        tile.GetComponent<Tile>().setState(Tile.State.notOwned);

        // in the corners of the map, there are no tiles
        if (noTiles(i, j))
        {
          tile.GetComponent<SpriteRenderer>().color = Color.black;
          tile.SetActive(false);
        }
      }
    }
    // center tile is owned by player
    Tiles[getTile(width / 2, width / 2)].GetComponent<Tile>().setState(Tile.State.Owned);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public int getTile(int x, int y)
  {
    if (noTiles(x, y))
    {
      return -1;
    }
    if (x < 0 || x >= width || y < 0 || y >= width)
    {
      return -1;
    }
    return x * width + y;
  }

  private bool noTiles(int x, int y)
  {
    // no corner tiles
    if (x == 0 && y == 0)
    {
      return true;
    }
    if (x == 0 && y == width - 1)
    {
      return true;
    }
    if (x == width - 1 && y == 0)
    {
      return true;
    }
    if (x == width - 1 && y == width - 1)
    {
      return true;
    }
    return false;
  }
}
