using UnityEngine;

public class WalkThroughWall : MonoBehaviour
{
    Collider C;
    LayerMask playerLayer;
    LayerMask reset;
    MaskSwitching MS;
    public LayerMask cube;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MS = GameObject.FindGameObjectWithTag("Player").GetComponent<MaskSwitching>();
        playerLayer = LayerMask.GetMask("Player");
        C = GetComponent<Collider>();
        cube += playerLayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (MS.wallMask)
        {
            C.excludeLayers = cube;
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
