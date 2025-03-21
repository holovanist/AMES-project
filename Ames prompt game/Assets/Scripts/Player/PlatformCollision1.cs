using UnityEngine;

public class PlatformCollision1 : MonoBehaviour
{
    public string playerTag = "Player";
    public Transform platform;
    public Transform parrent;

    private void Start()
    {
        parrent = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            parrent.transform.SetParent(platform);
            //other.gameObject.transform.parent = platform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            parrent.transform.SetParent(null);
            //other.gameObject.transform.parent = null;
        }
    }
}
