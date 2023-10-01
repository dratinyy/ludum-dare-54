using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
  public GameObject tilePrefab;

  public int width = 7;
  public float tileWidth = 4;

  private GameObject[] Tiles;

  // Start is called before the first frame update
  void Start()
  {
    // initialize Tiles 
    Tiles = new GameObject[width * width];
    // create tiles
    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < width; j++)
      {
        // create tile
        float xCoordinate = (i - width / 2) * tileWidth;
        float yCoordinate = (j - width / 2) * tileWidth;
        GameObject tile = Instantiate(tilePrefab, new Vector3(xCoordinate, yCoordinate, 1), Quaternion.identity, transform);
        Tiles[getTile(i, j)] = tile;

        // set menu not owned
        // center tile is owned by player
        if (i == width / 2 && j == width / 2)
        {
          tile.GetComponent<Tile>().initFirstDayCenter();
        }
        else
        {
          tile.GetComponent<Tile>().initFirstDay();
        }

        // in the corners of the map, there are no tiles
        if (noTiles(i, j))
        {
          tile.GetComponent<SpriteRenderer>().color = Color.black;
          tile.SetActive(false);
        }
      }
    }
  }

  // Update is called once per frame
  public void UpdateTiles()
  {
    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < width; j++)
      {
        // if tile is active 
        if (!noTiles(i, j))
        {
          // update tile
          Tile tile = Tiles[getTile(i, j)].GetComponent<Tile>();
          tile.UpdateWalkable();
          tile.GiveRentMoney();
        }
      }
    }
  }

  private int getTile(int x, int y)
  {
    return y * width + x;
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
