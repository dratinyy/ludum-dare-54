using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveConstants : MonoBehaviour
{

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

        // Count of enemy type 0 (starts at wave 0)
        enemyWaveCount[0] = waveNumber * 3 + 15;

        // Count of enemy type 1 (starts at wave 1)
        enemyWaveCount[1] = waveNumber * 3 - 3;

        // Count of enemy type 1 (starts at wave 3)
        enemyWaveCount[2] = waveNumber * 2 - 4;

        return enemyWaveCount;
    }

}
