using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;


public class PointsUi : MonoBehaviour
{
    public PointsCounter pointsCounter;
    public TextMeshProUGUI pointsText;

    private void Start()
    {
        UpdateUi();
    }

    public void UpdateUi()
    {
        pointsText.text = $"Очки {pointsCounter.points.ToString() }";
    }
}