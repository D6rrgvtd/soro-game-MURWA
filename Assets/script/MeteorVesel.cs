using UnityEngine;
using System.Collections;

public class MeteorVessel : MonoBehaviour, IWeapon
{
    [Header("設定")]
    public Camera playerCamera;
    public Camera meteorCamera;   // ← 新しく追加（メテオ用カメラ）

    public GameObject meteorPrefab;
    public float cameraLockDuration = 10f;  // ← ここを15秒に
    public float spawnHeight = 40f;
    public int meteorCount = 200;
    public float meteorInterval = 0.4f;
    public float meteorArea = 50f;
    public float tiltAngle = 45f;
    public int meteordown = 0;

    private bool isCasting = false;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (meteorCamera == null)
            meteorCamera = GameObject.Find("meteorcamera").GetComponent<Camera>();

        meteorCamera.enabled = false;
    }


    public void Attack()
    {
        if (isCasting) return;
        StartCoroutine(CastMeteor());
    }

    private IEnumerator CastMeteor()
    {
        isCasting = true;

        // メテオ落下の中心（プレイヤー前方10m）
        Vector3 targetGround = playerCamera.transform.position + playerCamera.transform.forward * 10f;
        Vector3 meteorSpawnPos = targetGround + Vector3.up * spawnHeight;

        // 位置と向きを MeteorCamera に合わせる
        meteorCamera.transform.position = meteorSpawnPos;
        meteorCamera.transform.LookAt(targetGround);

        // メテオ落下を先に開始
        StartCoroutine(SpawnDiagonalMeteors(targetGround));

        // 0.1秒後にカメラ切り替え
        yield return new WaitForSeconds(0.1f);

        playerCamera.enabled = false;
        meteorCamera.enabled = true;

        // 15秒維持
        yield return new WaitForSeconds(cameraLockDuration);

        // 元に戻す
        meteorCamera.enabled = false;
        playerCamera.enabled = true;

        isCasting = false;
      
    }

    private IEnumerator SpawnDiagonalMeteors(Vector3 targetGround)
    {
        for (int i = 0; i < meteorCount; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-meteorArea, meteorArea),
                0f,
                Random.Range(-meteorArea, meteorArea)
            );

            Vector3 groundPos = targetGround + randomOffset;
            Vector3 spawnPos = groundPos + Vector3.up * spawnHeight;

            Quaternion rot = Quaternion.LookRotation((groundPos - spawnPos).normalized);

            Instantiate(meteorPrefab, spawnPos, rot);

            meteordown++;

            yield return new WaitForSeconds(meteorInterval);
        }
        if (meteordown==meteorCount)
        {
            Destroy(gameObject);
        }

    }
}
