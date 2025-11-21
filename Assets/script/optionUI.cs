using UnityEngine;
using UnityEngine.SceneManagement;

public class optionUI : MonoBehaviour
{
    
    void Start()
    {
        // メニューでマウス操作できるようにする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnMainButton()
    {
        SceneManager.LoadScene("Main menu");
    }

   
}
