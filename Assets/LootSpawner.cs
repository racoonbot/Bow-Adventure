using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public List<GameObject> AllLootList = new List<GameObject>();
    [SerializeField] private float _heightSpawnPositionY = 9f;
    [SerializeField] private float _minSpawnPositionX = -6f;
    [SerializeField] private float _maxSpawnPositionX = 11f;

    [SerializeField] private Vector3 _spawnRandomPosition;

    [SerializeField] private float timer = 2f;


    void Start()
    {
        SpawnLoot();
    }


    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (Random.Range(0, 2) == 1)
            {
                SpawnLoot();
            }
                timer = 2f;
        }
    }

    private void GetSpawnRandomPosition()
    {
        _spawnRandomPosition =
            new Vector3(Random.Range(_minSpawnPositionX, _maxSpawnPositionX), _heightSpawnPositionY, 0);
    }

    private GameObject GetRandomLootForSpawn()
    {
        int obgIndex = Random.Range(0, AllLootList.Count);
        return AllLootList[obgIndex];
    }

    private void SpawnLoot()
    {
        GetSpawnRandomPosition();
        GameObject randomLoot = GetRandomLootForSpawn();
        if (randomLoot != null)
        {
            GameObject LootObject = Instantiate(randomLoot, _spawnRandomPosition, Quaternion.identity);
        }
    }
}