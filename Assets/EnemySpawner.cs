using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public EnemyHealth enemyHealth;
    public bool enemyInScene;

    [SerializeField] private float minSpawnPositionX = -1f;
    [SerializeField] private float maxSpawnPositionX = 10f;
    [SerializeField] private float minSpawnPositionY = -5f;
    [SerializeField] private float maxSpawnPositionY = 8f;


    void Start()
    {
        if (!enemyInScene)
        {
            EnemySpawn();
        }
        
    }


    void Update()
    {
    }

    private void OnEnable()
    {
        enemyHealth.OnDeathEnemy += EnemySpawn;
    }

    private void OnDisable()
    {
        enemyHealth.OnDeathEnemy -= EnemySpawn;
    }

    private void EnemySpawn()
    {
        GameObject EnemyObj = Instantiate(EnemyPrefab, GetRandomSpawnPositionXY(), Quaternion.identity);
        enemyInScene = true;
    }

    private Vector3 GetRandomSpawnPositionXY()
    {
        Vector3 newRandomPosition = new Vector3(Random.Range(minSpawnPositionX, maxSpawnPositionX),
            Random.Range(minSpawnPositionY, maxSpawnPositionY), 0f);
        return newRandomPosition;
    }
}