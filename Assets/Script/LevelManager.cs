using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public int level;
    public LevelUi levelUI;

    private void Start()
    {
        levelUI = FindObjectOfType<LevelUi>();
    }

    public void AddLevel()
    {
        level++;
        levelUI.UpdateUi();
        Debug.Log("level" + level);
    }

  
}
