using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTimer : MonoBehaviour
{
    private Player _player;

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    public void StartBuffTimer()
    {
        Debug.Log("Start Buff Timer");
        StartCoroutine(BuffCoroutine());
    }

    private IEnumerator BuffCoroutine()
    {
        _player.isBuffed = true;
        yield return new WaitForSeconds(5f); 
        _player.isBuffed = false; 
        Debug.Log("Buff has ended");
    }
}