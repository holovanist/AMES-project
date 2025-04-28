using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string levelToLoad = "Level 1";
    public string mainMenu = "Start Menu";
    public string HowToPlay = "HowToPlay";
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
        SceneManager.LoadScene(mainMenu);
    }
    public void HowToPlay1()
    {
        SceneManager.LoadScene(HowToPlay);
    }
    public void Controls()
    {
        SceneManager.LoadScene(Controls1);
    }
}