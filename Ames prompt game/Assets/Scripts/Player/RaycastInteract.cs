using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace player
{
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class RaycastInteract : MonoBehaviour
    {
        private StarterAssetsInputs _input;

        [SerializeField]
        float interactRange = 4;
        [SerializeField]
        TextMeshProUGUI InteractText;
        public float InteractDelay = 0.1f;
        RaycastHit hit;
        PlayerHealth PH;
        PlayerShoot PS;
        public float HealAmount;
        public int AmmoAdded;
        // Start is called before the first frame update
        void Start()
        {
            PS = GetComponent<PlayerShoot>();
            PH = GetComponent<PlayerHealth>();
            InteractText.enabled = false;
            _input = GetComponent<StarterAssetsInputs>();
        }

        // Update is called once per frame
        void Update()
        {
            //RaycastHit hit;
            Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, interactRange))
            {
                if (hit.collider.gameObject.CompareTag("Interactable"))
                {
                    InteractText.enabled = true;
                    if (_input.interact)
                    {
                        //needs a small cooldown
                        hit.collider.gameObject.GetComponent<Animator>().SetTrigger("Interact");
                        InteractText.enabled = false;
                    }
                }
                else if (hit.collider.gameObject.CompareTag("PickupItemAmmo"))
                {
                    InteractText.enabled = true;
                    if (_input.interact)
                    {
                        Invoke(nameof(PickupItemAmmo), InteractDelay);
                        InteractText.enabled = false;
                    }
                }
                else if (hit.collider.gameObject.CompareTag("PickupItemHealth"))
                {
                    InteractText.enabled = true;
                    if (_input.interact)
                    {
                        Invoke(nameof(PickupItemHealth), InteractDelay);
                        InteractText.enabled = false;
                    }
                }
                else
                {
                    InteractText.enabled = false;
                }
            }
            else
            {
                InteractText.enabled = false;
            }
        }

        public void PickupItemAmmo()
        {
            Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, interactRange))
                if (hit.collider.gameObject.CompareTag("PickupItemAmmo") && _input.interact)
                {
                PS.BulletsAvalible += AmmoAdded;
                hit.collider.gameObject.SetActive(false);
                }
        }
        public void PickupItemHealth()
        {
            Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, interactRange))
                if (hit.collider.gameObject.CompareTag("PickupItemHealth") && _input.interact)
                {
                hit.collider.gameObject.SetActive(false);
                PH.health += HealAmount;
                }
        }
    }
}