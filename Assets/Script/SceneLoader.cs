using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        Debug.Log(playerHealth != null ? "PlayerHealth найден." : "PlayerHealth не найден.");
    }
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }
    
    public void LoadSampleSceneScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void LoadGameOverScene()
    {
        Debug.Log("Загрузка сцены окончания игры.");
        SceneManager.LoadScene("EndScene");
    }

    private void OnEnable()
    {
        if (playerHealth != null)
        {
            Debug.Log("Подписка на событие OnDead успешно выполнена.");
            playerHealth.OnDead += LoadGameOverScene; //Не подписывается
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDead -= LoadGameOverScene;
            Debug.Log("Отписка на событие OnDead успешно выполнена.");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
