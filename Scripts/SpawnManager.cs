using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject goalPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveLevel = 1;

    private GameObject instantiatedGoal;

    void Start()
    {
        SpawnEnemyWave(2);
        SpawnGoal();
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    void Update()
    {

        //Wave manager
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveLevel++;
            print("Level: " + waveLevel + ". Wave%2: " + waveLevel%2);

            SpawnEnemyWave(waveLevel * 2);

            if (instantiatedGoal)
            {
                Destroy(instantiatedGoal);
            }
            SpawnGoal();

            if (waveLevel%2 == 0)
            {
                SpawnPowerup();
            }
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    Vector3 GenerateGoal()
    {
        int x = 13;
        if (waveLevel%2 == 0)
        {
            x = -13;
        }

        Vector3 randomPos = new Vector3(0, 0, x);
        return randomPos;
    }

    private void SpawnEnemyWave(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    private void SpawnGoal()
    {
        instantiatedGoal = Instantiate(goalPrefab, GenerateGoal(), powerupPrefab.transform.rotation);
    }
}
