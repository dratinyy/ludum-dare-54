using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public int waveNumber = 0;

    public GameObject waveSpawnerGO;

    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else if (_instance == null)
        {
            _instance = this;
            // Random.InitState(42069);
            waveSpawnerGO = Instantiate(Resources.Load<GameObject>("Prefabs/Gameplay/WaveSpawner"), Vector2.zero, Quaternion.identity);
        }
        waveSpawnerGO.GetComponent<WaveSpawner>().InitSpawn(waveNumber);
    }

    void Update()
    {
    }
}
