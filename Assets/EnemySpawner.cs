using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private List<Vector3> spawnes;
    [SerializeField] private float minSpawnPositionX = -1f;
    [SerializeField] private float maxSpawnPositionX = 10f;
    [SerializeField] private float minSpawnPositionY = -5f;
    [SerializeField] private float maxSpawnPositionY = 8f;

    void Start()
    {
        spawnes = new List<Vector3>()
        {
            new Vector3(0f, -2f, 0f),
            new Vector3(2f, 3f, 0f),
            new Vector3(5f, 1f, 0f),
            new Vector3(7f, -4f, 0f),
            new Vector3(3f, 5f, 0f),
            new Vector3(9f, 0f, 0f),
            new Vector3(1f, -3f, 0f),
            new Vector3(4f, 2f, 0f),
            new Vector3(8f, -1f, 0f),
            new Vector3(-1f, 4f, 0f)
        };
        
        EnemySpawn();
    }

    private void EnemySpawn()
    {
        GameObject enemyObj = Instantiate(EnemyPrefab, GetRandomSpawnPositionXY(), Quaternion.identity);
        EnemyHealth enemyHealth = enemyObj.GetComponent<EnemyHealth>();
        
        PointsCounter pointsCounter = FindObjectOfType<PointsCounter>(); //!
        PointsUi pointsUi = FindObjectOfType<PointsUi>();
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        
       
            enemyHealth.OnDeathEnemy += pointsCounter.AddPoints; 
            enemyHealth.OnDeathEnemy += pointsUi.UpdateUi;
            enemyHealth.OnDeathEnemy += levelManager.AddLevel;
        
        enemyHealth.OnDeathEnemy += EnemySpawn; 
    }

    private Vector3 GetRandomSpawnPositionXY()
    {
        Vector3 newSpawnPosition = spawnes[Random.Range(0, spawnes.Count)];
        return newSpawnPosition;
    }
}