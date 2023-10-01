using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHover : MonoBehaviour
{

    public GameObject normalDisplay;
    public GameObject hoverDisplay;
    public GameObject Tile;

    void Start()
    {
        normalDisplay.SetActive(true);
        hoverDisplay.SetActive(false);
    }

    public void OnHoverEnter()
    {
        normalDisplay.SetActive(false);
        hoverDisplay.SetActive(true);
    }

    public void OnHoverExit()
    {
        normalDisplay.SetActive(true);
        hoverDisplay.SetActive(false);
    }
}
