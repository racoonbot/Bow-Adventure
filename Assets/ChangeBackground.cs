using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    public SpriteRenderer backgroundSpriteRenderer;
    public List<Sprite> backgrounds = new List<Sprite>();

    
    public void NewBackground()
    {
        int index = Random.Range(0, backgrounds.Count);
        backgroundSpriteRenderer.sprite = backgrounds[index];
    }
}