using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{

    public int timesPlayed = 0;
    public int timesWon = 0;

    public int[] enemyKilled = new int[WaveConstants.enemyTypeCount];

    public int wavesSurvived;

    // Investments 
    public int moneyInvestedInUpgrades;
    public int moneyInvestedInWeapons;
    public int moneyInvestedInHeal;
    public int moneyInvestedInRocket;
    public int moneyInvestedInTiles;

    public int moneyGainedFromIncome;
    public int moneyGainedFromTileSales;
    public int moneyGainedFromTilesOwned;
    public int moneyGainedFromWeaponsOwned;
    public int moneyGainedFromRents;

    private static LeaderboardManager _instance;
    public static LeaderboardManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LeaderboardManager>();
            }
            return _instance;
        }
    }

    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            // persisted between games
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetValues()
    {
        timesPlayed++;
        print("times played: " + timesPlayed);

        // reset values
        for (int i = 0; i < enemyKilled.Length; i++)
        {
            enemyKilled[i] = 0;
        }
        wavesSurvived = 0;
        moneyInvestedInUpgrades = 0;
        moneyInvestedInWeapons = 0;
        moneyInvestedInHeal = 0;
        moneyInvestedInRocket = 0;
        moneyInvestedInTiles = 0;
        moneyGainedFromIncome = 0;
        moneyGainedFromTileSales = 0;
        moneyGainedFromTilesOwned = 0;
        moneyGainedFromWeaponsOwned = 0;
        moneyGainedFromRents = 0;
    }
}
