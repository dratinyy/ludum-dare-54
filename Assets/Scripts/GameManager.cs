using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public bool isDay = false;
    private GameObject globalLight;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private Transform player;
    public Transform Player
    {
        get
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return player;
        }
    }

    public int waveNumber = -1;

    public GameObject waveSpawnerGO;

    void Start()
    {
        // set global light
        globalLight = GameObject.FindGameObjectWithTag("GlobalLight");

        // place player in the center 
        Player.position = new Vector3(0, 0, 0);

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
        waveSpawnerGO = Instantiate(Resources.Load<GameObject>("Prefabs/Gameplay/WaveSpawner"), Vector2.zero, Quaternion.identity);
        waveSpawnerGO.SetActive(false);
        StartCoroutine(StartFirstNightCoroutine());
    }

    // void Update()
    // {
    //     // When spacebar is pressed
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         StartDay();
    //     }
    // }

    IEnumerator StartFirstNightCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartNight();
        UIManager.Instance.UpdateMoney(player.GetComponent<Player>().Money);
        UIManager.Instance.UpdateHealth(player.GetComponent<Player>().Health, player.GetComponent<Player>().MaxHealth);
    }


    public void StartNight()
    {
        isDay = false;
        waveNumber++;
        Debug.Log("Starting night " + waveNumber);
        UIManager.Instance.SetNightDisplay(waveNumber + 1);
        AudioManager.Instance.PlayRandomMusicNight();

        if (waveNumber > 0)
        {
            Player.position = new Vector3(0, 0, 0);
        }
        else
        {
            LeaderboardManager.Instance.ResetValues();
        }

        // Set global light intensity to  0.5 
        player.transform.Find("NightMask").gameObject.SetActive(true);

        // Spawn enemies or other night-time events
        waveSpawnerGO.SetActive(true);
        waveSpawnerGO.GetComponent<WaveSpawner>().InitSpawn(waveNumber);

        // player can shoot 
        Player.GetComponent<Player>().canShoot = true;
    }

    public void StartDay()
    {
        // Set state to Day
        isDay = true;
        Debug.Log("Starting day " + waveNumber);
        UIManager.Instance.SetDayDisplay(waveNumber + 1);
        AudioManager.Instance.PlayRandomMusicDay();

        if (waveNumber < EconomyConstants.numberOfWavesWithIncome)
        {
            Player.GetComponent<Player>().UpdateMoney(EconomyConstants.dailyIncome);
            LeaderboardManager.Instance.moneyGainedFromIncome += EconomyConstants.dailyIncome;
        }

        // Set player child NightMask object to inactive
        player.transform.Find("NightMask").gameObject.SetActive(false);
        // player cannot shoot 
        Player.GetComponent<Player>().canShoot = false;
    }

    public void Win()
    {
        AudioManager.Instance.PlayRandomMusicGoodEnding();
        LeaderboardManager.Instance.wavesSurvived = waveNumber + 1;
        LeaderboardManager.Instance.timesWon++;
        UIManager.Instance.WinRoutine();
    }

    public void GameOver()
    {
        AudioManager.Instance.PlayRandomMusicEnding();
        UIManager.Instance.GameOverRoutine();
    }
}
