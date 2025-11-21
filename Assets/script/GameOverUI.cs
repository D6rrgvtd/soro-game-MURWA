using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // リトライボタン用
    public void OnRetryButton()
    {
        // 前回のシーンをロード
        string lastScene = PlayerPrefs.GetString("LastScene", "seen main");
        SceneManager.LoadScene(lastScene);
    }

    // メインメニューボタン用
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("Main menu");
    }
}

