using UnityEngine;
using UnityEngine.SceneManagement;

public class loadlevel : MonoBehaviour
{
    public string levelToLoad;


    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(levelToLoad);
    }
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
