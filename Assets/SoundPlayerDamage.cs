using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlayerDamage : MonoBehaviour
{
    public AudioSource audioSource;
    public PlayerHealth playerHealth;

    public void PlayDamageSound()
    {
        audioSource.Play();
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        playerHealth.OnTouched += PlayDamageSound;
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        playerHealth.OnTouched -= PlayDamageSound;
    }
}