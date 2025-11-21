using UnityEngine;
using System.Collections.Generic;

public class BlackHoleEffect : MonoBehaviour
{
    [Header("吸引設定")]
    public float radius = 100f;          // 吸引半径
    public float pullForce = 1.5f;       // 引き寄せる力
    public float destroyDistance = 5f;  // 中心何mで消すか

    [Header("存在時間")]
    public float duration = 13f;        // ブラックホールの寿命

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject); // ブラックホール自体を消す
            return;
        }

        // ▼ 半径50m以内のコライダーを取得
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                // そのオブジェクトのRigidbody
                Rigidbody rb = hit.attachedRigidbody;
                if (rb != null)
                {
                    // ▼ ブラックホール中心へ引き寄せる
                    Vector3 direction = (transform.position - hit.transform.position).normalized;
                    rb.AddForce(direction * pullForce, ForceMode.Acceleration);
                }

                // ▼ 中心に来たらDestroy
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < destroyDistance)
                {
                    Destroy(hit.gameObject);
                }
            }
        }
    }

    // ▼ Sceneビューで半径可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
