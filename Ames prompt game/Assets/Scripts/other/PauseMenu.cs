using NewMovment;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string MainMenu;
    public Transform Transform;
    public GameObject player;
    public GameObject Cam;
    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Canvas>().enabled = false;
        Transform = GameObject.FindGameObjectWithTag("Scene Start Point").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            Resume();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GetComponent<Canvas>().enabled = true;
        Cam.GetComponent<PlayerCam>().enabled = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<Canvas>().enabled = false;
        Cam.GetComponent <PlayerCam>().enabled = true;
        Time.timeScale = 1;
    }

    public void ReloadLevel()
    {
        player.transform.position = Transform.position;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
       SceneManager.LoadScene(MainMenu);
    }
    public void SaveGame()
    {
        player.GetComponent<SaveScript>().Save();
    }

    private void OnLevelWasLoaded(int level)
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        Transform = GameObject.FindGameObjectWithTag("Scene Start Point").transform;
    }
}
