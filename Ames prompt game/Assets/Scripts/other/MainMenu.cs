using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string levelToLoad = "Level 1";
    public string MainMenu1 = "Start Menu";
    public string HowToPlay1 = "HowToPlay";
    public string Controls1 = "Controls";
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
    public void HowToPlay()
    {
        SceneManager.LoadScene(HowToPlay1);
    }
    public void Controls()
    {
        SceneManager.LoadScene(Controls1);
    }
    
}