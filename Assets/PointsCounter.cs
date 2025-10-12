using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    public int points;
    public EnemyHealth _enemyHealth;

    void Start()
    {
        points = 0;
    }

    public void AddPoints()
    {
        points++;
        Debug.Log(points);
    }
}