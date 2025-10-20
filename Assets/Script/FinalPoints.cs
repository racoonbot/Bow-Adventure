using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FinalPoints : MonoBehaviour
{
   public PointsCounter pointsCounter;
   public TextMeshProUGUI text;

   private void Start()
   {
      text.text = pointsCounter.points.ToString();
   }
}
