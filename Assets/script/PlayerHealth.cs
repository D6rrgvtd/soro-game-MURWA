using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("体力設定")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI設定")]
    public Slider hpBar; 

    [Header("カメラ設定")]
    public Transform playerCamera;
    public float lookUpAngle = -60f;
    public float returnDuration = 1.5f;

    private bool isDead = false;
    private bool isCameraAnimating = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (hpBar != null)
            hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;

        if (playerCamera == null)
            playerCamera = Camera.main.transform;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (hpBar != null)
            hpBar.value = currentHealth;

        Debug.Log($"プレイヤーのHP: {currentHealth}/{maxHealth}");

        if (!isCameraAnimating)
            StartCoroutine(CameraHitReaction());

        if (currentHealth <= 0)
            Die();
    }


    IEnumerator CameraHitReaction()
    {
        isCameraAnimating = true;
        Quaternion startRot = playerCamera.localRotation;
        Quaternion upRot = Quaternion.Euler(lookUpAngle, 0, 0);
        float t = 0f;

        while (t < 0.3f)
        {
            t += Time.deltaTime;
            playerCamera.localRotation = Quaternion.Slerp(startRot, upRot, t / 0.3f);
            yield return null;
        }

        t = 0f;
        while (t < returnDuration)
        {
            t += Time.deltaTime;
            playerCamera.localRotation = Quaternion.Slerp(upRot, startRot, t / returnDuration);
            yield return null;
        }

        playerCamera.localRotation = startRot;
        isCameraAnimating = false;
    }

    void Die()
    {
        isDead = true;
        Debug.Log("死亡しました");
        FindObjectOfType<GameTimer>().StopTimer();
        SceneManager.LoadScene("Game Over Scenes");
    }


    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

}
