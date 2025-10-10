using System.Collections;
using UnityEngine;

public class LootShield : MonoBehaviour
{
    private bool isActiveShield = false;
    private GameObject player;
    private Transform shieldTransform;
    private float shieldTimer = 5f;

    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>().gameObject;
        // player.layer = LayerMask.NameToLayer("Player");
        shieldTransform = player.transform.Find("Shield");

        if (shieldTransform != null)
        {
            shieldTransform.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Щит не найден!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            ActivateShield(); 
            
            Destroy(other.gameObject); 
            Destroy(gameObject); 
        }
    }

    private void ActivateShield()
    {
        if (shieldTransform != null && !isActiveShield)
        {
            shieldTransform.gameObject.SetActive(true); 
            isActiveShield = true;
            Debug.Log("Щит включен.");
            StartCoroutine(ShieldTimer());
        }
    }

    private IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(shieldTimer);
        shieldTransform.gameObject.SetActive(false);  // Отключаем щит после таймера
        isActiveShield = false; // Устанавливаем состояние обратно
        Debug.Log("Щит отключен.");
    }
}