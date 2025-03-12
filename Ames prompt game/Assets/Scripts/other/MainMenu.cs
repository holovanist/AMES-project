using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string levelToLoad = "Level 1";
    public string MainMenu1 = "Start Menu";
    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene(MainMenu1);
    }
}