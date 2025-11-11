using TMPro;
using UnityEngine;

public class RecordPointsEndScreen : MonoBehaviour
{
    public TextMeshProUGUI text; 

    void Start()
    {
 
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        UpdateText(); 
    }

    public void UpdateText()
    {
  
        text.text = "Вы набрали " + GameManager.points.ToString() + " очков";
    }
}