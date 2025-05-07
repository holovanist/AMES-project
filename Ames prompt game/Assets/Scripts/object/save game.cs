using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
    string password = "1234567890";
    void Start()
    {

    }
    public void Save()
    {
        //string result = EncryptDecryptData("a");
        //Debug.Log(result);
        PlayerSaveData myData = new PlayerSaveData();
        myData.SceneName = SceneManager.GetActiveScene().name;
        string myDataString = JsonUtility.ToJson(myData);
        myDataString = EncryptDecryptData(myDataString);
        //Debug.Log(myDataString);
        string file = Application.persistentDataPath + "/" + gameObject.name + ".json";
        System.IO.File.WriteAllText(file, myDataString);
        //Debug.Log(file);
    }
    private void OnLevelWasLoaded(int level)
    {
        string scene;
        scene = SceneManager.GetActiveScene().name;
        Debug.Log(scene);
        if (scene != ("Controls") || scene != ("HowToPlay") || scene != ("Start Menu") || scene != ("WinScreen"))
        {
        Save();
            Debug.Log("1");
        }
    }
    public void Load()
    {
        string file = Application.persistentDataPath + "/" + gameObject.name + ".json";
        if (File.Exists(file))
        {
            string jsonData = File.ReadAllText(file);
            jsonData = EncryptDecryptData(jsonData);
            PlayerSaveData myData = JsonUtility.FromJson<PlayerSaveData>(jsonData);
            if (SceneManager.GetActiveScene().name != myData.SceneName)
            {
                SceneManager.LoadScene(myData.SceneName);
            }
            else
            {
            }
            Time.timeScale = 1;
            //string myData = File.ReadAllText(file);
            //myData = EncryptDecryptData(myData);
            ////Debug.Log(myData);
        }
    }
    public string EncryptDecryptData(string data)
    {
        string result = "";
        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ password[i % password.Length]);
        }
        return result;
    }
}

[System.Serializable]
public class PlayerSaveData
{
    public string SceneName;
}