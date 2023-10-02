using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private const float spawnInterval = 0.08f;
    private const float spawnDistanceFromOrigin = 20f;
    private float currentSpawnDistance;

    private int[] enemySpawnCounters;
    List<int> indexList;
    private bool spawning = false;
    private float clock = 0f;

    public void InitSpawn(int waveNumber)
    {
        enemySpawnCounters = WaveConstants.EnemyWaveCounts(waveNumber);
        spawning = true;
        currentSpawnDistance = spawnDistanceFromOrigin / 2f;

        // Create list of all mob types
        indexList = new List<int>();
        for (int i = 0; i < enemySpawnCounters.Length; i++)
        {
            for (int j = 0; j < enemySpawnCounters[i]; j++)
            {
                indexList.Add(i);
            }
        }
    }

    void Update()
    {
        if (spawning)
        {
            if (clock > spawnInterval)
            {
                SpawnEnemy();
                clock = 0f;
            }
            else
            {
                clock += Time.deltaTime;
            }
        }
        else if (transform.childCount == 0)
        {
            GameManager.Instance.StartDay();
            gameObject.SetActive(false);
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, indexList.Count);
        SpawnEnemyOfType(indexList[randomIndex]);
        indexList.RemoveAt(randomIndex);

        if (indexList.Count == 0)
            spawning = false;
    }

    void SpawnEnemyOfType(int type)
    {
        if (WaveConstants.modeChenille)
        {
            type = 1;
        }
        else if (WaveConstants.modeNecro)
        {
            type = 3;
        }
        float randomRotation = Random.Range(0, 2 * Mathf.PI);
        Vector2 spawnPosition = new Vector3(Mathf.Cos(randomRotation), Mathf.Sin(randomRotation), 0) * currentSpawnDistance;
        currentSpawnDistance = Mathf.Min(spawnDistanceFromOrigin, currentSpawnDistance + spawnDistanceFromOrigin / 200f);
        Instantiate(WaveConstants.enemyPrefabs[type], spawnPosition, Quaternion.identity, transform);
    }
}
