using UnityEngine;
using System.Collections;

public class tateburi : MonoBehaviour
{
    public float swingAngle = 120f; // U‚éŠp“x
    public float returnTime = 0.15f; // –ß‚éŠÔ
    public float swingTime = 0.1f;   // U‚èŠÔ

    private bool isAttacking = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        Quaternion startRot = transform.localRotation;
        Quaternion midRot = Quaternion.Euler(0,0,swingAngle);

        // U‚é
        float t = 0;
        while (t < swingTime)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(startRot, midRot, t / swingTime);
            yield return null;
        }

        // –ß‚·
        t = 0;
        while (t < returnTime)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(midRot, startRot, t / returnTime);
            yield return null;
        }

        isAttacking = false;
    }
}

