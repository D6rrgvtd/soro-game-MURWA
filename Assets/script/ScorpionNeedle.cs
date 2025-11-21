using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScorpionNeedle : MonoBehaviour, IWeapon
{
    [Header("攻撃設定")]
    public Camera playerCamera;
    public float range = 10f;
    public float damage = 10f;           // 初回ダメージ
    public float poisonDPS = 5f;         // 毒ダメージ（毎秒）
    public float poisonDuration = 10f;    // 毒持続時間（秒）

    // 毒中の敵を管理
    private List<Coroutine> activePoisons = new List<Coroutine>();

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    public void Attack()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    // 初回ダメージ
                    enemy.TakeDamage(damage);

                    // 毒付与（EnemyHealth は変更せず武器側で管理）
                    Coroutine c = StartCoroutine(PoisonCoroutine(enemy, poisonDuration, poisonDPS));
                    activePoisons.Add(c);
                }
            }
        }
    }

    private IEnumerator PoisonCoroutine(EnemyHealth enemy, float duration, float dps)
    {
        float timer = 0f;
        while (timer < duration && enemy != null)
        {
            timer += Time.deltaTime;
            enemy.TakeDamage(dps * Time.deltaTime);
            yield return null;
        }
        activePoisons.RemoveAt(0);
    }
}
