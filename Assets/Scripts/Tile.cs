using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isWalkable = false;
    private bool isCenter = false;
    private GameObject player;

    private bool isAvailable = false;

    public List<GameObject> neighbors = new List<GameObject>();
    
    private GameObject Player
    {
        get
        {
            if (player == null)
            {
                player = GameManager.Instance.Player.gameObject;
            }
            return player;
        }
    }

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

    public GameObject sellButton;
    public GameObject buyButton;
    public GameObject rentButton;
    public GameObject unRentButton;

    // Start is called before the first frame update
    void Start()
    {
        // Colors 
        notOwnedColor = overlay.GetComponent<SpriteRenderer>().color;
        // Gold color with 50% alpha
        RentedColor = new Color(1, 0.92f, 0.016f, 0.5f);

        rentedOverlay.SetActive(false);

        if (isWalkable)
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

    public bool getIsWalkable()
    {
        return isWalkable;
    }

    public void initFirstDay()
    {
        currentState = State.notOwned;
        overlay.SetActive(true);
        rentedOverlay.SetActive(false);
        setMenuNotOwned();
        isWalkable = false;
    }

    public void initFirstDayCenter()
    {
        currentState = State.Owned;
        overlay.SetActive(false);
        rentedOverlay.SetActive(false);
        isWalkable = true;
        isCenter = true;
        UpdateNeighborsAvailability();
    }

    public void setState(State newState)
    {
        currentState = newState;
        switch (newState)
        {
            case State.notOwned:
                overlay.SetActive(true);
                rentedOverlay.SetActive(false);
                setMenuNotOwned();
                UpdateNeighborsAvailability();
                break;
            case State.Owned:
                overlay.SetActive(false);
                rentedOverlay.SetActive(false);
                setMenuOwned();
                UpdateNeighborsAvailability();
                break;
            case State.Rented:
                overlay.SetActive(true);
                rentedOverlay.SetActive(true);
                setMenuRented();
                UpdateNeighborsAvailability();
                break;
        }
        updateAvailable();
    }

    public void UpdateWalkable()
    {
        if (GameManager.Instance.isDay)
        {
            isWalkable = true;
            transform.Find("Available").gameObject.SetActive(isAvailable);
        }
        else
        {
            transform.Find("Available").gameObject.SetActive(false);
            menu.SetActive(false);
            switch (currentState)
            {
                case State.notOwned:
                    isWalkable = false;
                    break;
                case State.Owned:
                    isWalkable = true;
                    break;
                case State.Rented:
                    isWalkable = false;
                    break;
            }
        }
        Physics2D.IgnoreCollision(GameManager.Instance.Player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), isWalkable);
    }

    public void GiveRentMoney()
    {
        if (GameManager.Instance.isDay && currentState == State.Rented)
        {
            Player.GetComponent<Player>().UpdateMoney(EconomyConstants.tileRentBenefit);
        }
    }

    public void setMenuOwned()
    {
        buyButton.SetActive(false);
        sellButton.SetActive(true);
        rentButton.SetActive(true);
        unRentButton.SetActive(false);
    }

    public void setMenuNotOwned()
    {
        buyButton.SetActive(true);
        sellButton.SetActive(false);
        rentButton.SetActive(false);
        unRentButton.SetActive(false);
    }

    public void setMenuRented()
    {
        buyButton.SetActive(false);
        sellButton.SetActive(false);
        rentButton.SetActive(false);
        unRentButton.SetActive(true);
    }

    public void openMenu()
    {
        menu.SetActive(false);
    }

    public void closeMenu()
    {
        menu.SetActive(false);
    }
    public void UpdateNeighborsAvailability()
    {
        foreach (GameObject neighbor in neighbors)
        {
            neighbor.GetComponent<Tile>().updateAvailable();
        }
    }

    public void setIsAvailable(bool available)
    {
        isAvailable = available;
        if(isAvailable)
        {
            transform.Find("Available").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("Available").gameObject.SetActive(false);
        }
    }

    public void updateAvailable()
    {
        // If owned or rented, not available
        if(currentState != State.notOwned)
        {
            setIsAvailable(false);
            return;
        }
        // If adjacent to owned or rented, available
        if(isAdjacentToOwnedOrRented())
        {
            setIsAvailable(true);
        }
        else 
        {
            setIsAvailable(false);
        }
    }

    void OnMouseOver()
    {
        if (isCenter || !GameManager.Instance.isDay)
        {
            return;
        }
        // Prevent raycast which happens when clicking on UI elements
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if(Input.GetMouseButtonDown(0) && ( currentState != State.notOwned || isAvailable))
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }
    }

    private bool isAdjacentToOwnedOrRented()
    {
        foreach (GameObject neighbor in neighbors)
        {
            if (neighbor.GetComponent<Tile>().currentState == State.Owned || neighbor.GetComponent<Tile>().currentState == State.Rented)
            {
                return true;
            }
        }
        return false;
    }

    public void buy()
    {
        if (Player.GetComponent<Player>().Money < EconomyConstants.tileBuyPrice)
        {
            return;
        }
        Player.GetComponent<Player>().UpdateMoney(-EconomyConstants.tileBuyPrice);
        setState(State.Owned);
        menu.SetActive(false);
    }

    public void rent()
    {
        setState(State.Rented);
        menu.SetActive(false);
    }

    public void unrent()
    {
        setState(State.Owned);
        menu.SetActive(false);
    }

    public void sell()
    {
        Player.GetComponent<Player>().UpdateMoney(EconomyConstants.tileSellPrice);
        setState(State.notOwned);
        menu.SetActive(false);
    }

}
