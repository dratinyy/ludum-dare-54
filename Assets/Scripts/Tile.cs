using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isWalkable = false;
    private GameObject notPosessedOverlay;
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        notPosessedOverlay = transform.GetChild(0).gameObject;
        if(isWalkable)
        {
          notPosessedOverlay.SetActive(false);
        }
        else
        {
          notPosessedOverlay.SetActive(true);
        }
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
        notPosessedOverlay.SetActive(false);
      }
      else
      {
        GetComponent<SpriteRenderer>().color = Color.black;
        notPosessedOverlay.SetActive(true);
      }
    }

    public bool getIsWalkable()
    {
      return isWalkable;
    }
}
