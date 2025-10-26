using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PointsCounter pointsCounter;

    void Start()
    {
        pointsCounter = FindObjectOfType<PointsCounter>();
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (!other.GetComponent<Bullet>())
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Bullet>())
        {
            pointsCounter.AddPoints(); 
            Destroy(gameObject); 
        }
    }
}