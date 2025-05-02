using UnityEngine;
using UnityEngine.SceneManagement;

public class reloadlevel : MonoBehaviour
{
    public Transform Transform;
    private void Start()
    {
        Transform = GameObject.FindGameObjectWithTag("Scene Start Point").transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = Transform.position;
    }
}
