using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
    public float autoLoadNextLevelAfterSeconds;
    void Start()
    {
        if (autoLoadNextLevelAfterSeconds <= 0)
            Debug.Log("Level auto load disabled, use positive number of seconds");
        else Invoke("LoadNextLevel", autoLoadNextLevelAfterSeconds);
    }
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}