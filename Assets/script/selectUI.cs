using UnityEngine;
using UnityEngine.SceneManagement;

public class selectUI : MonoBehaviour
{

    void Start()
    {
        // メニューでマウス操作できるようにする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnbattleButton()
    {
        SceneManager.LoadScene("seen main");
    }


    public void OnMainButton()
    {
        SceneManager.LoadScene("Main menu");
    }


}
