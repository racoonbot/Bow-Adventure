using System.Collections;
using UnityEngine;

public class LootShield : MonoBehaviour
{
    private Shield shield;
    private AudioSource audioSource;

    private void Start()
    {
        shield = FindObjectOfType<Shield>();
        audioSource = GetComponent<AudioSource>();
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            shield.ActivateShield();
            if (audioSource != null)
            {
                audioSource.Play();
                Debug.Log("Звук щита воспроизводится.");
                StartCoroutine(DestroyAfterDelay(2f)); // Уничтожаем через 2 секунды
            }
            else
            {
                Debug.LogError("AudioSource не найден.");
            }
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}