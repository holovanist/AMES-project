using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace player
{
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class RaycastShoot : MonoBehaviour
    {
        private StarterAssetsInputs _input;

        // Start is called before the first frame update
        void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (_input.shoot)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    //hit.point this is a script that only take the point that gets hit
                    //Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<Enemy.EnemyHP>().TakeDamage(1);
                    }
                }
            }
        }
    }
}