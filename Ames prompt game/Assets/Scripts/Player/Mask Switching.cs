using NewMovment;
using player;
using StarterAssets;
using TMPro;
using UnityEngine;

public class MaskSwitching : MonoBehaviour
{
    private StarterAssetsInputs _input;
    Grappling Grapple;
    Sliding Slide;
    WallRunning WR;
    Climbing Climb;
    Dashing Dash;
    public int mask = -1;
    public bool Mask1Collected = false;
    public bool Mask2Collected = false;
    public bool Mask3Collected = false;
    public bool Mask4Collected = false;
    public bool Mask5Collected = false;
    public bool PickUp;
    public bool wallMask;
    public bool floorMask;
    public TextMeshProUGUI mask1;
    public TextMeshProUGUI mask2;
    public TextMeshProUGUI mask3;
    public TextMeshProUGUI mask4;
    public TextMeshProUGUI mask5;
    public int masksCollected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        masksCollected = 0;
        Grapple = GetComponent<Grappling>();
        Slide = GetComponent<Sliding>();
        WR = GetComponent<WallRunning>();
        Climb = GetComponent<Climbing>();
        Dash = GetComponent<Dashing>();
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mask != 5 || mask! < 6)
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                mask++;
            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                if (mask == -1)
                    mask = 4;
                else
                    mask--;
            }
        }
        else
        {
            mask = -1;
        }

        if (mask == 0 && !Mask1Collected)
        {
            mask = -1;
        }
        else if (mask == 1 && !Mask2Collected)
        {
            mask = -1;
        }
        else if (mask == 2 && !Mask3Collected)
        {
            mask = -1;
        }
        else if (mask == 3 && !Mask4Collected)
        {
            mask = -1;
        }
        else if (mask == 4 && !Mask5Collected)
        {
            mask = -1;
        }
        else if (mask == 5)
        {
            mask = -1;
        }
        if (mask == -1)
        {
            Grapple.enabled = false;
            PickUp = false;
            Grapple.enabled = false;
            Slide.enabled = false;
            WR.enabled = false;
            Climb.enabled = false;
            Dash.enabled = false; 
            wallMask = false;
            floorMask = false;
            mask1.enabled = false;
            mask2.enabled = false;
            mask3.enabled = false;
            mask4.enabled = false;
            mask5.enabled = false;
        }
        else if (mask == 0 && Mask1Collected)
        {
            Grapple.enabled = true;
            PickUp = false;
            Slide.enabled = false;
            WR.enabled = false;
            Climb.enabled = false;
            Dash.enabled = false;
            wallMask = false;
            floorMask = false;
            mask1.enabled = true;
            mask2.enabled = false;
            mask3.enabled = false;
            mask4.enabled = false;
            mask5.enabled = false;
        }
        else if (mask == 1 && Mask2Collected)
        {
            Grapple.enabled = false;
            PickUp = false;
            Slide.enabled = true;
            WR.enabled = true;
            Climb.enabled = true;
            Dash.enabled = true;
            wallMask = false;
            floorMask = false;
            mask1.enabled = false;
            mask2.enabled = true;
            mask3.enabled = false;
            mask4.enabled = false;
            mask5.enabled = false;
        }
        else if (mask == 2 && Mask3Collected)
        {
            Grapple.enabled = false;
            PickUp = false;
            Slide.enabled = false;
            WR.enabled = false;
            Climb.enabled = false;
            Dash.enabled = false;
            wallMask = true ;
            floorMask = false;
            mask1.enabled = false;
            mask2.enabled = false;
            mask3.enabled = true;
            mask4.enabled = false;
            mask5.enabled = false;
        }
        else if (mask == 3 && Mask4Collected)
        {
            Grapple.enabled = false;
            PickUp = false;
            Slide.enabled = false;
            WR.enabled = false;
            Climb.enabled = false;
            Dash.enabled = false;
            wallMask = false;
            floorMask = true;
            mask1.enabled = false;
            mask2.enabled = false;
            mask3.enabled = false;
            mask4.enabled = true;
            mask5.enabled = false;
        }
        else if (mask == 4 && Mask5Collected)
        {
            Grapple.enabled = false;
            PickUp = true;
            Slide.enabled = false;
            WR.enabled = false;
            Climb.enabled = false;
            Dash.enabled = false;
            wallMask = false;
            floorMask = false;
            mask1.enabled = false;
            mask2.enabled = false;
            mask3.enabled = false;
            mask4.enabled = false;
            mask5.enabled = true;
        }
        
    }
}
