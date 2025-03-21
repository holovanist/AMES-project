using StarterAssets;
using System.Threading;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    //https://www.youtube.com/watch?v=fApXEL0xsx4
    bool isHolding = false;
    int click;
    float timer;
    public float clickCooldown = 1f;

    public float throwForce = 600f;
    public float maxDistance = 3f;
    float distance;

    TempParrent tp;
    Rigidbody rb;
    StarterAssetsInputs it;
    
    Vector3 objectPos;

    void Start()
    {
        click = 0;
        timer = clickCooldown;
        it = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
       rb = GetComponent<Rigidbody>();
        tp = TempParrent.instance;
    }

    void Update()
    {
        if(click == 1 && timer > 0)
            timer -= Time.deltaTime;
        if(timer <= 0)
        {
            click = 0;
            timer = clickCooldown;
        }    

        if (isHolding)
            Hold();
    }

    private void OnMouseOver()
    {
        if(tp!= null)
        {   
            if(it.Grab && !isHolding && click == 0)
            {
                click = 1;
                distance = Vector3.Distance(this.transform.position, tp.transform.position);
                if(distance <= maxDistance)
                {
                isHolding = true;
                rb.useGravity = false;
                rb.detectCollisions = true;

                this.transform.SetParent(tp.transform);

            }
            }
            else if (it.Grab && isHolding && click == 0)
            {
                click = 1;
                Drop();
            }
        }
        else
        {
            Debug.LogWarning("temp parrent item not found in scene");
        }
    }

    //private void OnMouseUp()
    //{
    //    Drop();
    //}

    private void OnMouseExit()
    {
        Drop();
    }
    private void Hold()
    {
        distance = Vector3.Distance(this.transform.position, tp.transform.position);

        if (distance >= maxDistance) Drop();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        //if(it.Grab && click == 0)
        if(Input.GetKeyDown(KeyCode.G))
        {
            //click = 1;
            rb.AddForce(tp.transform.forward * throwForce);
            Drop();
        }
        //else if (!it.Grab && click == 1 )
        //{
        //    click = 0;
        //}
    }

    private void Drop()
    {
        if(isHolding)
        {
            isHolding = false;
            objectPos = this.transform.position;
            this.transform.position = objectPos;
            this.transform.SetParent(null);
            rb.useGravity = true;
        }    
    }
}
