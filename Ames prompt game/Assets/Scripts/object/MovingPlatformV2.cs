using NewMovment;
using System.Collections;
using UnityEngine;

public class MovingPlatformV2 : MonoBehaviour
{
    public float speed = 10f;
    public float delay = 1f;
    public int startingPoint;
    public Transform[] points;
    public Transform parrent;
    public string playerTag = "playerCap";
    public Transform platform;
    [SerializeField]
    private int i;
    Rigidbody rb;
    PlayerMovement pm;

    bool isWaiting;

    private void Start()
    {
        transform.position = points[startingPoint].position;
        parrent = GameObject.FindGameObjectWithTag("Player").transform;
        pm = parrent.GetComponent<PlayerMovement>();
        rb = parrent.GetComponent<Rigidbody>();
    }


    private void Update()
    {
        Move();
    }


    private void Move()
    {
        if (!isWaiting)
        {
            if (Vector3.Distance(transform.position, points[i].position) > 0.02f)
            {
                
               
                transform.position = Vector3.MoveTowards
                    (transform.position, points[i].position, speed * Time.deltaTime);
            }
            else
            {
                isWaiting = true;
                StartCoroutine(changeDelay());
            }
        }
    }


    IEnumerator changeDelay()
    {
        yield return new WaitForSeconds(delay);
        if (i == points.Length -1)
        {
            i = 0;
        }
        else
        i++;
        isWaiting = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            pm.offMovingPlatform = false;
            rb.interpolation = RigidbodyInterpolation.Extrapolate;
            parrent.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            parrent.transform.SetParent(null);
        }
    }
}