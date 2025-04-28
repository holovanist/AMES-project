using UnityEngine;

public class Button : MonoBehaviour
{
    public bool pressed;
    public bool wasPressed;
    [HideInInspector]
    public bool isPressed;
    [HideInInspector]
    public bool isCurrentlyPressed;
    Animator anim;
    public string animationTriggerUp;
    public string animationTriggerDown;
    public ButtonController BC;

    private void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        BC.needsUpdate = true;
        isPressed = true;
        isCurrentlyPressed = true;
        pressed = true;
        wasPressed = true;
        if (anim != null)
            anim.SetTrigger(animationTriggerUp);
    }
    private void OnCollisionExit(Collision collision)
    {
        BC.needUpdate = true;
        isCurrentlyPressed=false;
        pressed = false;
        if (anim != null)
            anim.SetTrigger(animationTriggerDown);
    }
}
