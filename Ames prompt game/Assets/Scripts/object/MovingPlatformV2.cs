using NewMovment;
using System.Collections;
using TMPro;
using UnityEngine;

public class MovingPlatformV2 : MonoBehaviour
{
    public float speed = 10f;
    public float delay = 1f;
    public int startingPoint;
    public Transform[] points;
    public GameObject parrent;
    public string playerTag = "playerCap";
    public Transform platform;
    [SerializeField]
    private int i;
    Rigidbody rb;
    PlayerMovement pm;
    public bool rotationgPlatform;

    bool isWaiting;
    public bool moveBackwards;
    bool movingBackwards;

    private void Start()
    {
        if(!rotationgPlatform)
        transform.position = points[startingPoint].position;
        parrent = GameObject.FindGameObjectWithTag("Player");
        pm = parrent.GetComponent<PlayerMovement>();
        rb = parrent.GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if(!rotationgPlatform)
        Move();
    }


    private void Move()
    {
        if (!isWaiting && !rotationgPlatform)
        {
            if (Vector3.Distance(transform.position, points[i].position) > 0.02f)
            {
                
               
                transform.position = Vector3.MoveTowards
                    (transform.position, points[i].position, speed * Time.deltaTime);
            }
            else
            {
                isWaiting = true;
                StartCoroutine(ChangeDelay());
            }
        }
    }


    IEnumerator ChangeDelay()
    {
        yield return new WaitForSeconds(delay);
        if (i == points.Length -1)
        {
            if(!moveBackwards)
                i = 0;
            else if(moveBackwards)
            {
                movingBackwards = true;
                i--;
            }
        }
        else if (i == 0 && movingBackwards)
        {
            movingBackwards = false;
        }
        else
        {
            if (!movingBackwards)
                i++;
            else if (movingBackwards)
                i--;

        }
        isWaiting = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            pm.offMovingPlatform = false;
            rb.interpolation = RigidbodyInterpolation.None;
            parrent.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            parrent.GetComponent<PlayerSceneTransfer>().unparrent = false;
            parrent.transform.SetParent(null);
        }
    }
}