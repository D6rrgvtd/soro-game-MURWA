using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float clearTime;
    public int enemyCount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
    public void AddKillcount()
    {
    enemyCount++; 
    }


    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            SceneManager.LoadScene("Timeout");
        }
    }


}
