using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleShop : MonoBehaviour
{

    private bool isOpen = false;
    public GameObject closedShopButton;
    public GameObject openShopButton;

    public void setClosed() {
        isOpen = false;
        closedShopButton.SetActive(true);
        openShopButton.SetActive(false);
    }

    public void setOpen() {
        isOpen = true;
        closedShopButton.SetActive(false);
        openShopButton.SetActive(true);
    }
}
