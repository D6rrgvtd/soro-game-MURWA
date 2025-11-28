using UnityEngine;
using System.Collections;

public class GunRecoil : MonoBehaviour
{
    public float recoilAngle = 8f;   // 反動の角度（上にどれだけ揺れるか）
    public float recoilSpeed = 0.05f; // 上に跳ねる速さ
    public float returnSpeed = 0.1f;  // 元に戻る速さ

    private bool isRecoiling = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Recoil());
        }
    }

    IEnumerator Recoil()
    {
        if (isRecoiling) yield break; // ← 反動途中に連打したら無視
        isRecoiling = true;

        Quaternion startRot = transform.localRotation;
        Quaternion recoilRot = startRot * Quaternion.Euler(0,0,-recoilAngle);

        // 反動（上に跳ねる）
        float t = 0;
        while (t < recoilSpeed)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(startRot, recoilRot, t / recoilSpeed);
            yield return null;
        }

        // 戻る
        t = 0;
        while (t < returnSpeed)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(recoilRot, startRot, t / returnSpeed);
            yield return null;
        }

        isRecoiling = false;
    }
}
