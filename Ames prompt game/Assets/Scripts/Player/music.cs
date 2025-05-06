using UnityEngine;

public class music : MonoBehaviour
{
    public AudioSource music1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        music1.playOnAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!music1.isPlaying)
        {
            music1.Play();
        }

    }
}
