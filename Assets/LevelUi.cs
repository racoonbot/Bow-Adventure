using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class LevelUi : MonoBehaviour
{
    public TextMeshProUGUI LevelText;
    public LevelManager levelManager;

    private void Start()
    {
        UpdateUi();
    }

    public void UpdateUi()
    {
        LevelText.text = $"Уровень {levelManager.level.ToString()}"; 
    }
}
