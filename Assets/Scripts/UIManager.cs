using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private static GameObject canvas;

    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else if (_instance == null)
        {
            _instance = this;
            Init();
        }
    }

    void Init()
    {
        canvas = GameObject.Find("Canvas");
        for (int i = 0; i < WeaponConstants.weaponStats.Length; i++)
        {
            canvas.transform.Find("Shop").Find("Weapons").GetChild(i).Find("Price").GetComponent<UnityEngine.UI.Text>().text =
                EconomyConstants.weaponPrices[i].ToString();
        }
        for (int i = 0; i < EconomyConstants.bonusStats.Length; i++)
        {
            canvas.transform.Find("Shop").Find("Stats").GetChild(i).Find("Price").GetComponent<UnityEngine.UI.Text>().text =
                EconomyConstants.bonusStats[i].price.ToString();
            canvas.transform.Find("Shop").Find("Stats").GetChild(i).Find("Aquired").GetComponent<UnityEngine.UI.Text>().text = "0 / " + EconomyConstants.bonusStats[i].maxQuantity.ToString();

        }
        canvas.transform.Find("Shop").Find("Heal").Find("Burger").Find("Price").GetComponent<UnityEngine.UI.Text>().text = EconomyConstants.burgerPrice.ToString();
        canvas.transform.Find("Shop").Find("Heal").Find("Burger").Find("Heal").GetComponent<UnityEngine.UI.Text>().text = "+" + EconomyConstants.burgerHealth.ToString();
        canvas.transform.Find("Shop").Find("Heal").Find("Healpack").Find("Price").GetComponent<UnityEngine.UI.Text>().text = EconomyConstants.healpackPrice.ToString();
        canvas.transform.Find("Shop").Find("Heal").Find("Healpack").Find("Heal").GetComponent<UnityEngine.UI.Text>().text = "+" + EconomyConstants.healpackHealth.ToString();
        canvas.transform.Find("Shop").Find("Spaceship").Find("Spaceship").Find("Price").GetComponent<UnityEngine.UI.Text>().text = EconomyConstants.spaceshipPrice.ToString();
    }

    // Flash screen red when damage is taken
    public void FlashScreen()
    {
        // GameObject flash = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Flash"), canvas.transform);
        // flash.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        // Destroy(flash, 0.1f);
    }

    public void UpdateMoney(float amount)
    {
        canvas.transform.Find("Money").gameObject.SetActive(true);
        canvas.transform.Find("Money").Find("MoneyText").GetComponent<UnityEngine.UI.Text>().text = amount.ToString();
    }

    public void UpdateHealth(float health, float maxHealth)
    {
        canvas.transform.Find("Health").gameObject.SetActive(true);
        canvas.transform.Find("Health").Find("HealthText").GetComponent<UnityEngine.UI.Text>().text = health.ToString();
        canvas.transform.Find("Health").Find("MaxHealthText").GetComponent<UnityEngine.UI.Text>().text = "/ " + maxHealth.ToString();
    }

    public void DisplayShop(bool display)
    {
        canvas.transform.Find("Shop").gameObject.SetActive(display);
    }

    public void SetNightDisplay(int waveNumber)
    {
        canvas.transform.Find("NextWaveButton2").gameObject.SetActive(false);

        Transform DayNightIndicator = canvas.transform.Find("DayNightIndicator");
        DayNightIndicator.Find("DayNightText").GetComponent<UnityEngine.UI.Text>().text = "Night " + waveNumber.ToString();
        DayNightIndicator.Find("day").gameObject.SetActive(false);
        DayNightIndicator.Find("night").gameObject.SetActive(true);

        if (waveNumber > 1)
        {
            GameObject.FindGameObjectsWithTag("TilesManager")[0].GetComponent<TilesManager>().UpdateTiles();
        }

        if (waveNumber == 1)
        {
            canvas.transform.Find("FirstNightHelper").gameObject.SetActive(true);
            StartCoroutine(DestroyFirstNightHelper());
        }
        else if (waveNumber == 2)
        {
            GameObject.Destroy(canvas.transform.Find("FirstDayHelper").gameObject);
        }
        else if (waveNumber > 2)
        {
            if (waveNumber == 4)
            {
                GameObject.Destroy(canvas.transform.Find("ShopHelper").gameObject);
            }
            if (waveNumber == EconomyConstants.numberOfWavesWithIncome + 2)
            {
                GameObject.Destroy(canvas.transform.Find("NoMoreIncomeHelper").gameObject);
            }
            canvas.transform.Find("ShopButton 1").gameObject.GetComponent<HandleShop>().setClosed();
            DisplayShop(false);
        }
    }

    IEnumerator DestroyFirstNightHelper()
    {
        yield return new WaitForSeconds(15f);
        GameObject.Destroy(canvas.transform.Find("FirstNightHelper").gameObject);
    }

    public void SetDayDisplay(int waveNumber)
    {
        canvas.transform.Find("NextWaveButton2").gameObject.SetActive(true);
        Transform DayNightIndicator = canvas.transform.Find("DayNightIndicator");
        DayNightIndicator.Find("DayNightText").GetComponent<UnityEngine.UI.Text>().text = "Day " + waveNumber.ToString();
        DayNightIndicator.Find("day").gameObject.SetActive(true);
        DayNightIndicator.Find("night").gameObject.SetActive(false);
        GameObject.FindGameObjectsWithTag("TilesManager")[0].GetComponent<TilesManager>().UpdateTiles();

        if (waveNumber == 1)
        {
            canvas.transform.Find("FirstDayHelper").gameObject.SetActive(true);
        }
        else if (waveNumber > 1)
        {
            Transform shopButton = canvas.transform.Find("ShopButton 1");
            if (waveNumber == 3)
            {
                shopButton.gameObject.SetActive(true);
                canvas.transform.Find("ShopHelper").gameObject.SetActive(true);
            }
            if (waveNumber == EconomyConstants.numberOfWavesWithIncome + 1)
            {
                canvas.transform.Find("NoMoreIncomeHelper").gameObject.SetActive(true);
            }
            shopButton.gameObject.GetComponent<HandleShop>().setOpen();
        }
    }
}
