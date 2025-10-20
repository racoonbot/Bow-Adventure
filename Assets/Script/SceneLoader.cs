using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
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
            playerHealth.OnDead += LoadGameOverScene;
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDead -= LoadGameOverScene;
        }
    }
}
