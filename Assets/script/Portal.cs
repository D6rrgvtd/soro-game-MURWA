using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    public ProceduralTerrain terrainGenerator;
    public float fadeDuration = 1f;
    public CanvasGroup fadeCanvas;
    public TimeOfDayManager timeManager;
    private bool isWarping = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isWarping)
        {
          
            StartCoroutine(WarpToNewWorld());
        }
    }


    IEnumerator WarpToNewWorld()
    {
        isWarping = true;

        // フェードアウト
        if (fadeCanvas != null)
            yield return StartCoroutine(Fade(1f));

        // 地形生成
        terrainGenerator.seed = Random.Range(0, 100000);
        terrainGenerator.GenerateTerrain();

        // 生成完了まで少し待機（必要に応じて調整）
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        // ポータルをランダム位置に移動
        float randomX = Random.Range(0f, terrainGenerator.width);
        float randomZ = Random.Range(0f, terrainGenerator.depth);
        float randomY = terrainGenerator.GetHeightAtPosition(randomX, randomZ) + 1f;
        transform.position = new Vector3(randomX, randomY, randomZ);

        if (timeManager != null)
        {
            timeManager.NextTimeOfDay();
        }

        // プレイヤー取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // プレイヤーをポータルの上空に移動
            player.transform.position = transform.position + Vector3.up * 10f;
         
        }





        // フェードイン
        if (fadeCanvas != null)
            yield return StartCoroutine(Fade(0f));

        isWarping = false;


    }



    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvas.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = targetAlpha;
    }
}
