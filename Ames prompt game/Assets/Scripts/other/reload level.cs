using UnityEngine;
using UnityEngine.SceneManagement;

public class reloadlevel : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
