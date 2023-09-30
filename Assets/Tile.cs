using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isWalkable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setIsWalkable(bool isWalkable)
    {
      this.isWalkable = isWalkable;
      // change color
      if(isWalkable)
      {
        GetComponent<SpriteRenderer>().color = Color.white;
      }
      else
      {
        GetComponent<SpriteRenderer>().color = Color.black;
      }
    }
}
