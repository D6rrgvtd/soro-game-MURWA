using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("‘Ì—Í")]
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"{gameObject.name}‚ÌHP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        Debug.Log($"{gameObject.name}‚ªŽ€–SI");
        GameManager.instance.AddKillcount();
        Destroy(gameObject);
    }
}
