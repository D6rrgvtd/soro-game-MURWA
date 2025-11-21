using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInController : MonoBehaviour
{
    public Image fadeImage; // 黒いImage
    public float fadeDuration = 1.5f; // フェードイン時間

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color color = fadeImage.color;
        float t = 0f;

        // 最初は真っ黒に
        color.a = 1f;
        fadeImage.color = color;

        // 徐々に透明に
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;

        // 完全に透明になったらImageを無効化
        fadeImage.gameObject.SetActive(false);
    }
}
