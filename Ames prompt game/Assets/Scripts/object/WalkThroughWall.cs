using UnityEngine;

public class WalkThroughWall : MonoBehaviour
{
    Collider C;
    LayerMask playerLayer;
    LayerMask reset;
    public bool q;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLayer = LayerMask.GetMask("Player");
        C = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (q)
        {
            C.excludeLayers = playerLayer;
        }
        else
        {
            C.excludeLayers = reset;
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        playerLayer = GameObject.FindGameObjectWithTag("Player").layer;
    }
}
