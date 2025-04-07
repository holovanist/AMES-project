using UnityEngine;

public class WalkOnFloor : MonoBehaviour
{
    Collider C;
    LayerMask playerLayer;
    LayerMask reset;
    MaskSwitching MS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MS = GameObject.FindGameObjectWithTag("Player").GetComponent<MaskSwitching>();
        playerLayer = LayerMask.GetMask("Player");
        C = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MS.floorMask)
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
