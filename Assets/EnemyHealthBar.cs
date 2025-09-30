using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image enemyHealthBar;
    [SerializeField] private EnemyHealth enemyHealth;

    private void Start()
    {
 
        enemyHealthBar ??= GameObject.Find("EnemyHealthBar")?.GetComponent<Image>();
        enemyHealth ??= GameObject.Find("EnemyHealth")?.GetComponent<EnemyHealth>();

        if (enemyHealthBar == null || enemyHealth == null)
        {
            Debug.LogError($"EnemyHealthBar: missing reference. enemyHealthBar={(enemyHealthBar==null)}," +
                           $" enemyHealth={(enemyHealth==null)} on {gameObject.name}");
            enabled = false;
        }
    }

    private void Update()
    {
        enemyHealthBar.fillAmount = enemyHealth.currentHealth / enemyHealth.maxHealth;
    }
}