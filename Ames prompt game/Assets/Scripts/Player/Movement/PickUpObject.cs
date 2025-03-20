using StarterAssets;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    //https://www.youtube.com/watch?v=6bFCQqabfzo
    [Header("Pickup Settings")]
    public Transform holdArea;
    [SerializeField]
    private GameObject heldObj;
    private Rigidbody heldObjRB;
    private StarterAssetsInputs it;
    public Transform cam;

    [Header("Physics Parameters")]
    public float pickupRange = 5.0f;
    public float pickupForce = 150.0f;
    public float pickupDrag = 10.0f;
    public float dragAfterDrop = 1.0f;
    public bool freezeRotation;
    [SerializeField]
    bool inputDown;
    bool down;

    private void Start()
    {
        it = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        if(it.Grab) inputDown = true;
        else inputDown = false;
        if(inputDown) down = true;
        if (inputDown && down)
        {
            if(heldObj == null)
            {
                RaycastHit hit;
                if(Physics.Raycast(cam.position, cam.forward, out hit, pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        if(heldObj != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(transform.position, holdArea.position) > 0.1f)
        {
            Vector3 movedir = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(movedir * pickupForce, ForceMode.Force);
        }
    }
    void PickupObject(GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.linearDamping = pickupDrag;
            if(freezeRotation) heldObjRB.constraints =RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }
    void DropObject()
    {
        down = true;
        heldObjRB.useGravity = true;
            heldObjRB.linearDamping = dragAfterDrop;
            if (freezeRotation) heldObjRB.constraints = RigidbodyConstraints.None;

            heldObjRB.transform.parent = null;
            heldObj = null;
       
    }
}
