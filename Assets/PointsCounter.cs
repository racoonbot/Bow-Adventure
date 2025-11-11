using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    public int points;
    private PointsUi pointsUI;

    void Start()
    {
        pointsUI = FindObjectOfType<PointsUi>();
        points = 0;
    }

    public void AddPoints()
    {
        points++;
        GameManager.AddPoints();
        pointsUI.UpdateUi();
    }
}