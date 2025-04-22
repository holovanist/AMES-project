using NewMovment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject[] buttons;
    [SerializeField]
    private int i;
    public bool needsUpdate;
    public bool needUpdate;
    Animator anim;
    public string animationTriggerUp;
    public string animationTriggerDown;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (buttons != null)
        {
            ButtonUpdater();
        }    
    }

    public void ButtonUpdater()
    {
        if (i == buttons.Length)
        {
            anim.SetTrigger(animationTriggerUp);
        }
        else
        {
            anim.SetTrigger(animationTriggerDown);
        }
        if (i < 0)
        {
            //i = 0;
        }

        foreach (var button in buttons)
        {
            
            if (button.GetComponent<Button>().pressed == true && needsUpdate)
            {
                i++;
                needsUpdate = false;
            }
            if (button.GetComponent<Button>().pressed == false &&needUpdate)
            {
                i--;
                needUpdate = false;
            }
        }
    }
}