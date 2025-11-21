using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    public Text timeText;
    public Text killText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        timeText.text = $"TIME : {GameManager.instance.clearTime:0.00}";
        killText.text = $"KILL : {GameManager.instance.enemyCount}";
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene("seen main"); 
    }

    public void OnTitleButton()
    {
        SceneManager.LoadScene("Main menu");
    }
}

