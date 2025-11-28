using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    void Start()
    {
        // メニューでマウス操作できるようにする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnStartButton()
    {
        // ゲームシーンに移動
        SceneManager.LoadScene("main menu"); 
    }

    public void OnOptionButton()
    {
        SceneManager.LoadScene("Option");
    }


    public void OnQuitButton()
    {
        // ゲーム終了
        Debug.Log("ゲーム終了");
        Application.Quit();

        // エディタで動作確認用
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}


