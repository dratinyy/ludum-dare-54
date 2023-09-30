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
        waveSpawnerGO.GetComponent<WaveSpawner>().InitSpawn(waveNumber);
    }

    void Update()
    {
    }
}
