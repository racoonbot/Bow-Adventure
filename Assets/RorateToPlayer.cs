using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RorateToPlayer : MonoBehaviour
{
    public Transform PlayerTransform;
    public EnemyShot EnemyShot;

    private void Start()
    {
        Vector3 directionToPlayer = EnemyShot.GetComponent<EnemyShot>().shootDir;
    }

    void Update()
    {
        //     if (PlayerTransform != null)
        //     {
        //         // Обновляем направление к игроку
        //         directionToPlayer = PlayerTransform.position - transform.position;
        //         directionToPlayer.z = 0; // Игнорируем ось Z для поворота по оси X
        //
        //         // Проверяем, что направление не нулевое
        //         if (directionToPlayer.sqrMagnitude > 0.0001f)
        //         {
        //             // Поворачиваем объект к игроку
        //             transform.up = directionToPlayer.normalized; // Устанавливаем направление вверх объекта
        //         }
        //     }
        // }
    }
}