using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private int points;
    public EnemyHealth _enemyHealth;

    void Start()
    {
        points = 0;
    }

    private void OnEnable()
    {
        if (_enemyHealth != null)
        {
            _enemyHealth.OnDeathEnemy += AddPoints;
        }
    }

    private void OnDisable()
    {
        if (_enemyHealth != null)
        {
            _enemyHealth.OnDeathEnemy -= AddPoints;
        }
    }

    private void AddPoints()
    {
        points++;
        Debug.Log(points);
    }
}