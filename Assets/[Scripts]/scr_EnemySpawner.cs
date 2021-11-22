/* Sourcefile:      scr_EnemySpawner.cs
 * Author:          Sam Pollock
 * Student Number:  101279608
 * Last Modified:   October 24th, 2021
 * Description:     Instantiates aliens and rockets during gameplay
 * Last edit:       Set up variables for gameplay relevent values.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_EnemySpawner : MonoBehaviour
{
    [Header("Alien")]
    public GameObject enemyPrefab;
    public float enemySpawnCooldown;

    [Header("Rocket")]
    public GameObject rocketPrefab;
    public float rocketSpawnCooldown;


    private float timeUntilEnemySpawn;
    private float timeUntilRocketSpawn;


    void Start()
    {
        timeUntilRocketSpawn = Time.time + (rocketSpawnCooldown / 2);
    }


    void Update()
    {
        EnemiesSpawning();
    }


    /// <summary>
    /// Spawn enemies and rockets according to timers
    /// </summary>
    void EnemiesSpawning()
    {
        if (Time.time > timeUntilEnemySpawn)
        {
            SpawnEnemy();
        }
        if (Time.time > timeUntilRocketSpawn)
        {
            SpawnRocket();
        }
    }

    /// <summary>
    /// Instantiate an enemy and reset timer
    /// </summary>
    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        timeUntilEnemySpawn = Time.time + enemySpawnCooldown;
    }

    /// <summary>
    /// Instantiate a rocket and reset timer
    /// </summary>
    void SpawnRocket()
    {
        GameObject rocket = Instantiate(rocketPrefab, new Vector3(-3, 2, 0), Quaternion.identity);
        timeUntilRocketSpawn = Time.time + rocketSpawnCooldown;
    }

}
