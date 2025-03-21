using UnityEngine;

public class TempParrent : MonoBehaviour
{
    public static TempParrent instance {  get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
