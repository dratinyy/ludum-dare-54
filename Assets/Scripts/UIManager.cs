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

    public void SetNightDisplay(int waveNumber)
    {
        canvas.transform.Find("NextWaveButton2").gameObject.SetActive(false);
        // GameObject.Find("ShopButton").GetComponent<HandleShop>().setClose();    
        GameObject.Find("DayNightText").GetComponent<UnityEngine.UI.Text>().text = "Night " + waveNumber.ToString();
        if (waveNumber == 1)
        {
            canvas.transform.Find("FirstNightHelper").gameObject.SetActive(true);
            StartCoroutine(DestroyFirstNightHelper());
        }
        else if (waveNumber == 2)
        {
            GameObject.Destroy(canvas.transform.Find("FirstDayHelper").gameObject);
        }
    }

    IEnumerator DestroyFirstNightHelper()
    {
        yield return new WaitForSeconds(15f);
        GameObject.Destroy(canvas.transform.Find("FirstNightHelper").gameObject);
    }

    public void SetDayDisplay(int waveNumber)
    {
        // GameObject.Find("ShopButton").GetComponent<HandleShop>().setOpen();    
        canvas.transform.Find("NextWaveButton2").gameObject.SetActive(true);
        GameObject.Find("DayNightText").GetComponent<UnityEngine.UI.Text>().text = "Day " + waveNumber.ToString();
        if (waveNumber == 1)
        {
            canvas.transform.Find("FirstDayHelper").gameObject.SetActive(true);
        }
    }
}
