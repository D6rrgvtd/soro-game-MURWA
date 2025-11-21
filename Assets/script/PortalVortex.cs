using UnityEngine;

public class PortalVortex : MonoBehaviour
{
    [Header("Vortex Settings")]
    public float rotationSpeed = 100f;
    public float waveSpeed = 2f;
    public float waveHeight = 0.1f;
    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        // âÒì]
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Ç‰ÇÁÇ‰ÇÁè„â∫Ç…îgë≈Ç¬ä¥Ç∂
        float scaleOffset = Mathf.Sin(Time.time * waveSpeed) * waveHeight;
        transform.localScale = startScale + new Vector3(scaleOffset, 0, scaleOffset);
    }
}
