using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveConstants : MonoBehaviour
{

    public static readonly EnemyWaveStats[] enemyWaveStats = new EnemyWaveStats[]
    {
        // Zombie
        new EnemyWaveStats
        {
            startAtWave = 0,
            intialCount = 4,
            additionalPerWave = 5
        },
        // Worm
        new EnemyWaveStats
        {
            startAtWave = 2,
            intialCount = 1,
            additionalPerWave = 3
        }, 
        // Golem
        new EnemyWaveStats
        {
            startAtWave = 4,
            intialCount = 1,
            additionalPerWave = 1
        },
        // Necromancer
        new EnemyWaveStats
        {
            startAtWave = 7,
            intialCount = 1,
            additionalPerWave = 1
        },
        // Boss
        new EnemyWaveStats
        {
            startAtWave = 10,
            intialCount = 1,
            additionalPerWave = 1
        }
    };

    public static int enemyTypeCount = 3;
    public static GameObject[] enemyPrefabs;

    void Start()
    {
        enemyPrefabs = new GameObject[enemyTypeCount];
        for (int i = 0; i < enemyTypeCount; i++)
        {
            enemyPrefabs[i] = Resources.Load<GameObject>("Prefabs/Enemy/Enemy" + i);
        }
    }


    // Returns an array of enemy counts for each enemy type for a given wave number
    // Example: EnemyWaveCounts(3) returns [21, 6, 2]
    public static int[] EnemyWaveCounts(int waveNumber)
    {
        int[] enemyWaveCount = new int[enemyTypeCount];

        for (int i = 0; i < enemyTypeCount; i++)
        {
            if (enemyWaveStats[i].startAtWave > waveNumber)
            {
                enemyWaveCount[i] = 0;
            }
            else
            {
                enemyWaveCount[i] = enemyWaveStats[i].intialCount + enemyWaveStats[i].additionalPerWave * (waveNumber - enemyWaveStats[i].startAtWave);
            }
        }

        return enemyWaveCount;
    }

    public class EnemyWaveStats
    {
        public int startAtWave;
        public int intialCount;
        public int additionalPerWave;
    }

}
