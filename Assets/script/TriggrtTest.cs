using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER: " + other.name);
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("STAY: " + other.name);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT: " + other.name);
    }
}
