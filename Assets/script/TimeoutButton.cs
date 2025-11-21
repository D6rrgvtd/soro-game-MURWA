using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeoutButton : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void OnRetryButton()
    {
        SceneManager.LoadScene("seen main");
    }

    public void OnmainbackButton()
    {
        SceneManager.LoadScene("Main menu");
    }

}
