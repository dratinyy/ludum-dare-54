using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMenuButton : MonoBehaviour
{

    public GameObject normalDisplay;
    public GameObject hoverDisplay;
    public GameObject Tile;

    public string type;

    void OnMouseOver()
    {
        // print("mouseOver");
        if (Input.GetMouseButtonDown(0))
        {
            print("Tile menu button clicked");
            if (type == "rent")
            {
                Tile.GetComponent<Tile>().rent();
            }
            else if (type == "buy")
            {
                Tile.GetComponent<Tile>().buy();
            }
            else if (type == "sell")
            {
                Tile.GetComponent<Tile>().sell();
            }
        }
    }

    void OnMouseEnter()
    {
        normalDisplay.SetActive(false);
        hoverDisplay.SetActive(true);
    }

    void OnMouseExit()
    {
        normalDisplay.SetActive(true);
        hoverDisplay.SetActive(false);
    }

}
