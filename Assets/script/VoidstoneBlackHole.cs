using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VoidstoneBlackHole : MonoBehaviour, IWeapon
{
    [Header("ブラックホールのプレハブ")]
    public GameObject blackHolePrefab;

    [Header("振動設定")]
    public float shakeStrength = 0.3f;     
    public float shakeSpeed = 30f;         
    public float shakeDuration = 7f;       

    [Header("生成位置オフセット")]
    public Vector3 spawnOffset = new Vector3(0, 0, 3f);

    private bool isCasting = false;
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    // ▼ PlayerAttack から呼ばれる
    public void Attack()
    {
        if (!isCasting)
            StartCoroutine(BlackHoleSequence());
    }

    IEnumerator BlackHoleSequence()
    {
        isCasting = true;

        // ▼ 7秒間激しく揺らす
        yield return StartCoroutine(ShakeOnly());

        // ▼ ブラックホール生成
        SpawnBlackHole();

        isCasting = false;
    }

    // ▼ 7秒間揺らすだけ（フェード削除）
    IEnumerator ShakeOnly()
    {
        float timer = 0f;
        Vector3 camOriginalPos = cam.localPosition;

        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            float shakeX = Mathf.Sin(Time.time * shakeSpeed) * shakeStrength;
            float shakeY = Mathf.Cos(Time.time * shakeSpeed * 1.2f) * shakeStrength;

            cam.localPosition = camOriginalPos + new Vector3(shakeX, shakeY, 0);

            yield return null;
        }

        // 元の位置に戻す
        cam.localPosition = camOriginalPos;
    }

    // ▼ ブラックホール生成
    void SpawnBlackHole()
    {
        if (blackHolePrefab == null)
        {
            Debug.LogWarning("Black Hole Prefab が設定されていません");
            return;
        }

        Vector3 pos = transform.position + transform.forward * spawnOffset.z;

        Instantiate(blackHolePrefab, pos, Quaternion.identity);

        Destroy(gameObject);
    }
}
