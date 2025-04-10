using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerSceneTransfer : MonoBehaviour
{
    public GameObject SpawnPos;
    public GameObject menu;
    public GameObject player;
    public GameObject player2;

    public bool OriginalPlayer = false;
    int count;
    float time;
    bool level1;
    public bool unparrent;

    // Start is called before the first frame update
    void Start()
    {
        level1 = true;
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("playerCap");
        menu = GameObject.FindGameObjectWithTag("menu");
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point");
        if (menu != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            if (GetComponentInChildren<Canvas>() != null)
                GetComponentInChildren<Canvas>().enabled = false;
        }
        if (menu == null)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            if (GetComponentInChildren<Canvas>() != null)
            GetComponentInChildren<Canvas>().enabled = true;
        }
        if (SpawnPos != null) gameObject.transform.position = SpawnPos.transform.position;
    }

    void Awake()
    {
            if (CompareTag("Player"))
            {
                DontDestroyOnLoad(this.gameObject);
            }
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point");
        if (SpawnPos != null)gameObject.transform.position = SpawnPos.transform.position;
    }
    private void Update()
    {
        if (!unparrent)
        {
            DontDestroyOnLoad(this.gameObject);
            unparrent = true;
        }
        if (level1)
        time += Time.deltaTime;
        if (time > .25f)
        {
            if (OriginalPlayer == false && player != null && GameObject.FindGameObjectsWithTag("Player").Count() <= 1)
            {
                OriginalPlayer = true;
                time = 0;
                level1 = false;
            }
            else if (OriginalPlayer == false && player != null)
            {
                Destroy(gameObject);
                time = 0;
                level1 = false;
            }
            else
            {
                time = 0;
                level1 = false;
            }
        }

        if(count == 1)
        {
            player.transform.localPosition = new Vector3(0, 0, 0);
            count = 0;
            gameObject.transform.position = SpawnPos.transform.position;
            
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        level1 = true;
        menu = GameObject.FindGameObjectWithTag("menu");

        if (OriginalPlayer == false)
        {
            Destroy(gameObject);
        }
        if (menu != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            if (GetComponentInChildren<Canvas>() != null)
                GetComponentInChildren<Canvas>().enabled = false;
        }
        if (menu == null)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            if (GetComponentInChildren<Canvas>() != null)
                GetComponentInChildren<Canvas>().enabled = true;
        }
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point");
        count = 1;
    }
}
