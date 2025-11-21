using System.Data;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatAmplitude = 0.3f;
    public float floatSpeed = 2f;
    public float rotationSpeed = 50f;

    private Vector3 startPos;
    private bool isActive = true;
    void Start()
    {
      startPos = transform.position;    
    }

    void Update()
    {
        if(!isActive) return;
    float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
    transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void StopFloating()
    {
        isActive = false;
    }

}
