using UnityEngine;
using UnityEngine.SceneManagement;

public class backbotton : MonoBehaviour
{
    void Start()
    { 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnmainButton()
    {
        SceneManager.LoadScene("main menu");
    }

}
