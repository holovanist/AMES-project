using NewMovment;
using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //public GameObject pointA;
    //public GameObject pointB;
    public float speed = 10f;
    public float delay = 1f;
    public Transform parrent;
    public string playerTag = "playerCap";
    //public GameObject platform;

    //private Vector3 targetPosition;

    public Transform startPoint, endPoint;

    private Transform destinationTarget, departTarget;

    private float startTime;

    private float journeyLength;

    bool isWaiting;

    private void Start()
    {
        departTarget = startPoint;
        destinationTarget = endPoint;
        parrent = GameObject.FindGameObjectWithTag("Player").transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position);
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {
        if(!isWaiting)
        {
            if(Vector3.Distance(transform.position, destinationTarget.position) > 0.01f)
            {
                float distCovered = (Time.time - startTime) * speed;

                float fractionOfJourney = distCovered / journeyLength;

                transform.position = Vector3.Lerp(departTarget.position, destinationTarget.position, fractionOfJourney);
            }
            else
            {
                isWaiting = true;
                StartCoroutine(changeDelay());
            }
        }
    }

    private void ChangeDestination()
    {
         if(departTarget == endPoint && destinationTarget == startPoint)
        {
            departTarget = startPoint;
            destinationTarget = endPoint;
        }
         else
        {
            departTarget = endPoint;
            destinationTarget = startPoint;
        }
    }

    IEnumerator changeDelay()
    {
        yield return new WaitForSeconds(delay);
        ChangeDestination();
        startTime = Time.time;
        journeyLength = Vector3.Distance(departTarget.position, destinationTarget.position );
        isWaiting = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            parrent.transform.SetParent(transform);
            //other.gameObject.transform.parent = platform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            parrent.transform.SetParent(null);
            //other.gameObject.transform.parent = null;
        }
    }


    /*void Start()
    {
        platform.transform.position = pointA.transform.position;
        targetPosition = pointB.transform.position;
        StartCoroutine(MovePlatform());
    }

    void Update()
    {
        
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            while((targetPosition - platform.transform.position).sqrMagnitude > 0.01f)
            {
                platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            targetPosition = targetPosition == pointA.transform.position ? pointB.transform.position : pointA.transform.position;

            yield return new WaitForSeconds(delay);
        }
    }*/
}
