using UnityEngine;
using System.Collections;

public class PlayerCameraShake : MonoBehaviour
{
    public Camera playerCamera;
    public float lookUpAngle = 45f; // è„Ç…å¸Ç≠äpìx
    public float recoverTime = 1.5f;

    private bool isRecovering = false;

    public void KnockUpView()
    {
        if (!isRecovering && playerCamera != null)
        {
            StartCoroutine(LookUpAndRecover());
        }
    }

    private IEnumerator LookUpAndRecover()
    {
        isRecovering = true;

        Quaternion startRot = playerCamera.transform.localRotation;
        Quaternion upRot = Quaternion.Euler(lookUpAngle, 0, 0);

        // è„Ç…å¸ÇØÇÈ
        float t = 0f;
        while (t < 0.25f)
        {
            t += Time.deltaTime;
            playerCamera.transform.localRotation = Quaternion.Slerp(startRot, upRot, t / 0.25f);
            yield return null;
        }

        // Ç‰Ç¡Ç≠ÇËñﬂÇ∑
        t = 0f;
        while (t < recoverTime)
        {
            t += Time.deltaTime;
            playerCamera.transform.localRotation = Quaternion.Slerp(upRot, startRot, t / recoverTime);
            yield return null;
        }

        isRecovering = false;
    }
}
