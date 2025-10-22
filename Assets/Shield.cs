using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldTimer = 5f;
    public GameObject shieldObject;
    
    private bool isActiveShield = false;
    private Coroutine shieldCoroutine;
    
    public void ActivateShield()
    {
        if (!isActiveShield)
        {
            if (shieldCoroutine != null)
            {
                StopCoroutine(shieldCoroutine);
            }
            shieldCoroutine = StartCoroutine(ShieldTimer());
        }
    }

    private IEnumerator ShieldTimer()
    {
        Debug.Log("Щит включен.");
        shieldObject.gameObject.SetActive(true); 
        isActiveShield = true;
        yield return new WaitForSeconds(shieldTimer);
        shieldObject.gameObject.SetActive(false);  // Отключаем щит после таймера
        isActiveShield = false; // Устанавливаем состояние обратно
        Debug.Log("Щит отключен.");
    }
}
