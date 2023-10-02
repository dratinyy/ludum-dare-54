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
            if (waveNumber == 3)
            {
                GameObject.Destroy(canvas.transform.Find("ShopHelper").gameObject);
                GameObject.Destroy(canvas.transform.Find("ShopArrowHelper").gameObject);
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
            if (waveNumber == 2)
            {
                shopButton.gameObject.SetActive(true);
                canvas.transform.Find("ShopHelper").gameObject.SetActive(true);
                canvas.transform.Find("ShopArrowHelper").gameObject.SetActive(true);
            }
            if (waveNumber == EconomyConstants.numberOfWavesWithIncome + 1)
            {
                canvas.transform.Find("NoMoreIncomeHelper").gameObject.SetActive(true);
            }
            shopButton.gameObject.GetComponent<HandleShop>().setOpen();
        }
    }

    IEnumerator GameOverCoroutine()
    {
        launchFadeOut();
        yield return new WaitForSeconds(2.5f);
        DisplayLoseScreen();
    }

    IEnumerator WinCoroutine()
    {
        launchFadeOut();
        yield return new WaitForSeconds(2.5f);
        DisplayWinScreen();
    }

    public void GameOverRoutine()
    {
        StartCoroutine("GameOverCoroutine");
    }

    public void WinRoutine()
    {
        StartCoroutine("WinCoroutine");
    }

    public void launchFadeOut()
    {
        canvas.transform.Find("GameOverFadeOut").GameObject().SetActive(true);
    }

    public void DisplayLoseScreen()
    {
        canvas.transform.Find("GameOverScreen").Find("Defeat").gameObject.SetActive(true);
        canvas.transform.Find("GameOverScreen").Find("Victory").gameObject.SetActive(false);
        canvas.transform.Find("GameOverScreen").Find("Flavor").GetComponent<UnityEngine.UI.Text>().text = "\"" + TextConstants.loseFunTexts[Random.Range(0, TextConstants.winFunTexts.Length)] + "\"";
        canvas.transform.Find("GameOverScreen").Find("Nights Survived").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = GameManager.Instance.waveNumber.ToString();
        UpdateLeaderboard();
    }

    public void DisplayWinScreen()
    {
        canvas.transform.Find("GameOverScreen").Find("Defeat").gameObject.SetActive(false);
        canvas.transform.Find("GameOverScreen").Find("Victory").gameObject.SetActive(true);
        canvas.transform.Find("GameOverScreen").Find("Flavor").GetComponent<UnityEngine.UI.Text>().text = "\"" + TextConstants.winFunTexts[Random.Range(0, TextConstants.winFunTexts.Length)] + "\"";
        canvas.transform.Find("GameOverScreen").Find("Nights Survived").Find("Amount").GetComponent<UnityEngine.UI.Text>().text =
        (GameManager.Instance.waveNumber + 1).ToString();
        UpdateLeaderboard();
    }

    private void UpdateLeaderboard()
    {
        for (int i = 0; i < WaveConstants.enemyTypeCount; i++)
        {
            if (LeaderboardManager.Instance.enemyKilled[i] > 0)
            {
                canvas.transform.Find("GameOverScreen").Find("Enemies").Find("Enemy" + i).gameObject.SetActive(true);
                canvas.transform.Find("GameOverScreen").Find("Enemies").Find("Enemy" + i).Find("Count").GetComponent<UnityEngine.UI.Text>().text = LeaderboardManager.Instance.enemyKilled[i].ToString() + " kills";
            }
            else
            {
                canvas.transform.Find("GameOverScreen").Find("Enemies").Find("Enemy" + i).gameObject.SetActive(false);
            }
        }


        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyInvestedInUpgrades").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "-$" + LeaderboardManager.Instance.moneyInvestedInUpgrades.ToString();
        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyInvestedInWeapons").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "-$" + LeaderboardManager.Instance.moneyInvestedInWeapons.ToString();
        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyInvestedInHeal").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "-$" + LeaderboardManager.Instance.moneyInvestedInHeal.ToString();
        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyInvestedInRocket").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "-$" + LeaderboardManager.Instance.moneyInvestedInRocket.ToString();
        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyInvestedInTiles").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "-$" + LeaderboardManager.Instance.moneyInvestedInTiles.ToString();

        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyGainedFromIncome").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "+$" + LeaderboardManager.Instance.moneyGainedFromIncome.ToString();
        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyGainedFromTileSales").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "+$" + LeaderboardManager.Instance.moneyGainedFromTileSales.ToString();
        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyGainedFromTilesOwned").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "+$" + LeaderboardManager.Instance.moneyGainedFromTilesOwned.ToString();
        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyGainedFromWeaponsOwned").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "+$" + LeaderboardManager.Instance.moneyGainedFromWeaponsOwned.ToString();
        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyGainedFromRents").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "+$" + LeaderboardManager.Instance.moneyGainedFromRents.ToString();
        canvas.transform.Find("GameOverScreen").Find("Money").Find("moneyOwned").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = "+$" + GameManager.Instance.Player.GetComponent<Player>().Money.ToString();

        int netWorth = 0 - LeaderboardManager.Instance.moneyInvestedInUpgrades - LeaderboardManager.Instance.moneyInvestedInWeapons
        - LeaderboardManager.Instance.moneyInvestedInHeal - LeaderboardManager.Instance.moneyInvestedInRocket
        - LeaderboardManager.Instance.moneyInvestedInTiles + LeaderboardManager.Instance.moneyGainedFromIncome
        + LeaderboardManager.Instance.moneyGainedFromTileSales + LeaderboardManager.Instance.moneyGainedFromTilesOwned
        + LeaderboardManager.Instance.moneyGainedFromWeaponsOwned + LeaderboardManager.Instance.moneyGainedFromRents
        + GameManager.Instance.Player.GetComponent<Player>().Money;

        canvas.transform.Find("GameOverScreen").Find("Games played").Find("Amount").GetComponent<UnityEngine.UI.Text>().text =
        LeaderboardManager.Instance.timesPlayed.ToString();

        canvas.transform.Find("GameOverScreen").Find("Successful escapes").Find("Amount").GetComponent<UnityEngine.UI.Text>().text =
        LeaderboardManager.Instance.timesWon.ToString();

        canvas.transform.Find("GameOverScreen").Find("Net Worth").Find("Amount").GetComponent<UnityEngine.UI.Text>().text = netWorth.ToString();
        canvas.transform.Find("GameOverScreen").gameObject.SetActive(true);
    }
}
