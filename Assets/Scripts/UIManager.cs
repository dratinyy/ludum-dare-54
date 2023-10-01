using System.Collections;
using System.Collections.Generic;
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
        }
    }

    // Flash screen red when damage is taken
    public void FlashScreen()
    {
        // GameObject flash = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Flash"), canvas.transform);
        // flash.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        // Destroy(flash, 0.1f);
    }

    public void UpdateHealth(float health, float maxHealth)
    {
        GameObject.Find("HealthText").GetComponent<UnityEngine.UI.Text>().text = health.ToString();
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
            if (waveNumber == 3)
            {
                //TODO: shop helper
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

        if (waveNumber == 1)
        {
            canvas.transform.Find("FirstDayHelper").gameObject.SetActive(true);
        }
        else if (waveNumber > 1)
        {
            Transform shopButton = canvas.transform.Find("ShopButton 1");
            if (waveNumber == 2)
            {
                shopButton.gameObject.SetActive(true);
                //TODO: shop helper
            }
            shopButton.gameObject.GetComponent<HandleShop>().setOpen();
        }
    }
}
