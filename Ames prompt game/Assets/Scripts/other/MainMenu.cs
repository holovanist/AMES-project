using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string levelToLoad = "Level 1";
    string levelToLoad2 = "Level 1 Hard";
    public string Select = "DifficultySelect";
    public string MainMenu1 = "Start Menu";
    public string HowToPlay1 = "HowToPlay";
    public string Controls1 = "Controls";
    
    public void StartEasyGame()
    {
        SceneManager.LoadScene(levelToLoad);
        Time.timeScale = 1.0f;
    }
    public void StartHardGame()
    {
        SceneManager.LoadScene(levelToLoad2);
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
    public void DifficultySelect()
    {
        SceneManager.LoadScene(Select);
    }
    
}