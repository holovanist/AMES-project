using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class CamSceneTransfer : MonoBehaviour
{
    public GameObject SpawnPos;
    public GameObject Cam;

    public bool OriginalPlayer = false;
    int count;
    float time;
    bool level1;

    // Start is called before the first frame update
    void Start()
    {
        level1 = true;
        Cam = GameObject.FindGameObjectWithTag("Cam");
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point");
    }

    void Awake()
    {
            if (CompareTag("Cam"))
            {
                DontDestroyOnLoad(this.gameObject);
            }
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point");
        if (SpawnPos != null)gameObject.transform.position = SpawnPos.transform.position;
    }
    private void Update()
    {
        if(level1)
        time += Time.deltaTime;
        if (time > .25f)
        {
            if (OriginalPlayer == false && Cam != null && GameObject.FindGameObjectsWithTag("Cam").Count() <= 1)
            {
                OriginalPlayer = true;
                time = 0;
                level1 = false;
            }
            else if (OriginalPlayer == false && Cam != null)
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
            Cam.transform.localPosition = new Vector3(0, 0, 0);
            count = 0;
            gameObject.transform.position = SpawnPos.transform.position;
            
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        level1 = true;

        if (OriginalPlayer == false)
        {
            Destroy(gameObject);
        }
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point");
        count = 1;
    }
}
