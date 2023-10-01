using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleShop : MonoBehaviour
{

    private bool isOpen = false;
    private bool isToggled = false;
    public GameObject closedShopButton;
    public GameObject openShopButton;

    public void setClosed()
    {
        isOpen = false;
        isToggled = false;
        closedShopButton.SetActive(true);
        openShopButton.SetActive(false);
    }

    public void setOpen()
    {
        isOpen = true;
        closedShopButton.SetActive(false);
        openShopButton.SetActive(true);
    }

    public void ToggleShop()
    {
        if (isOpen)
        {
            isToggled = !isToggled;
            UIManager.Instance.DisplayShop(isToggled);
        }
    }
}
