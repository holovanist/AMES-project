using NewMovment;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement pm;
    private StarterAssetsInputs it;
    public Transform cam;
    public Transform gunTip;
    public LayerMask WhatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCooldown;
    private float grapplingCdTimer;

    private bool grappling;
    //bool buttonPressed;
    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        it = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
           //if(it.Grapple) buttonPressed = true;
           //else buttonPressed = false;
           if(it.Grapple /*&& buttonPressed*/) StartGrapple();

           if (grapplingCdTimer > 0)
               grapplingCdTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if(grappling)
            lr.SetPosition(0,gunTip.position);
    }

    private void StartGrapple()
    {
        if(grapplingCdTimer > 0) return;

        grappling = true;

        pm.freeze = true;

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, maxGrappleDistance, WhatIsGrappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1,grapplePoint);
    }
    private void ExecuteGrapple()
    {
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointReletiveYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointReletiveYPos + overshootYAxis;

        if(grapplePointReletiveYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }
    public void StopGrapple()
    {
        pm.freeze = false;
        grappling = false;

        grapplingCdTimer = grapplingCooldown;

        lr.enabled = false; 
    }
}
