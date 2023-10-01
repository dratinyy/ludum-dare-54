using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isWalkable = false;

    public enum State 
    {
        notOwned, 
        Owned,
        Rented
    };

    // Colors 
    public Color notOwnedColor;
    // public Color SelectedColor;
    public Color RentedColor;

    private State currentState = State.notOwned;

    public GameObject overlay;
    
    public GameObject menu;

    public GameObject rentedOverlay;
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Colors 
        notOwnedColor = overlay.GetComponent<SpriteRenderer>().color;
        // Gold color with 50% alpha
        RentedColor = new Color(1, 0.92f, 0.016f, 0.5f);

        rentedOverlay.SetActive(false);

        if(isWalkable)
        {
          overlay.SetActive(false);
        }
        else
        {
          overlay.SetActive(true);
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
        overlay.SetActive(false);
      }
      else
      {
        GetComponent<SpriteRenderer>().color = Color.black;
        overlay.SetActive(true);
      }
    }

    public bool getIsWalkable()
    {
      return isWalkable;
    }

    public void setState(State newState)
    {
        currentState = newState;
        switch(newState)
        {
            case State.notOwned:
            overlay.SetActive(true);
            rentedOverlay.SetActive(false);
            overlay.GetComponent<SpriteRenderer>().color = notOwnedColor;
            setIsWalkable(false);
            break;
            case State.Owned:
            overlay.SetActive(false);
            rentedOverlay.SetActive(false);
            setIsWalkable(true);
            break;
            case State.Rented:
            overlay.SetActive(true);
            rentedOverlay.SetActive(true);
            overlay.GetComponent<SpriteRenderer>().color = RentedColor;
            setIsWalkable(false);
            break;
        }
    }
    void OnMouseOver()
    {
        if(!GameManager.Instance.isDay)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(menu.activeSelf)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }
    }
    public void buy()
    {
        print("buying");
        setState(State.Owned);
        menu.SetActive(false);
    }

    public void rent()
    {
        print("renting");
        setState(State.Rented);
        menu.SetActive(false);
    }

    public void sell()
    {
        print("selling");
        setState(State.notOwned);
        menu.SetActive(false);
    }

}
