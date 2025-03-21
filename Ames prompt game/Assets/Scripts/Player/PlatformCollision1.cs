using UnityEngine;

public class PlatformCollision1 : MonoBehaviour
{
    public string playerTag = "Player";
    public Transform platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            other.gameObject.transform.parent = platform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals (playerTag))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
