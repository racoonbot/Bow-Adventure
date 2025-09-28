using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealthBar ??= GameObject.Find("PlayerHealthBar")?.GetComponent<Image>();
        playerHealth ??= GameObject.Find("Player")?.GetComponent<PlayerHealth>();

        if (playerHealthBar == null || playerHealth == null)
        {
            Debug.LogError($"HealthBar: missing reference. playerHealthBar={(playerHealthBar==null)}, " +
                           $"playerHealth={(playerHealth==null)} on {gameObject.name}");
            enabled = false;
        }
    }

    private void Update()
    {
        playerHealthBar.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;
    }
}