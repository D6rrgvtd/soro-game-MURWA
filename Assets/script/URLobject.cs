using UnityEngine;

public class URLobject : MonoBehaviour
{
    public string url = "";//‚±‚±‚ÉURL

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Application.OpenURL(url);
        }
    }
}

