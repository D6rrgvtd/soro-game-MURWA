using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageAmount = 10f;
    public float damageInterval = 1f;

    private float lastDamageTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    Debug.Log(" プレイヤーにダメージを与えた！");
                }

                lastDamageTime = Time.time;
            }
        }
    }
}

