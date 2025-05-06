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
        MaskSwitching _maskSwitching;

        [SerializeField]
        float interactRange = 4;
        [SerializeField]
        TextMeshProUGUI InteractText;
        public TextMeshProUGUI InteractText1;
        public TextMeshProUGUI InteractText2;
        public float InteractDelay = 0.1f;
        RaycastHit hit;
        int collectablesCollected;
        string number;
        // Start is called before the first frame update
        void Start()
        {
            if( InteractText != null )
            InteractText.enabled = false;
            InteractText1.enabled = false;
            _input = GetComponent<StarterAssetsInputs>();
            _maskSwitching = GetComponent<MaskSwitching>();
        }

        // Update is called once per frame
        void Update()
        {
            number = collectablesCollected.ToString();
            InteractText2.SetText(number + "/ 5 collectables collected");
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
                        Invoke(nameof(PickupItem), InteractDelay);
                        if (InteractText1 != null)
                            InteractText1.enabled = false;
                    }
                }
                else if (hit.collider.gameObject.CompareTag("PickupItemMask"))
                {
                    if(InteractText != null)
                    InteractText.enabled = true;
                    if (_input.interact)
                    {
                        Invoke(nameof(PickupItemAmmo), InteractDelay);
                        if (InteractText != null)
                            InteractText.enabled = false;
                    }
                }
                else
                {
                    if (InteractText != null)
                        InteractText.enabled = false;
                }
            }
            else
            {
                if (InteractText != null)
                    InteractText.enabled = false;
            }
        }

        public void PickupItemAmmo()
        {
            Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, interactRange))
                if (hit.collider.gameObject.CompareTag("PickupItemMask") && _input.interact)
                {
                    if (!_maskSwitching.Mask1Collected)
                        _maskSwitching.Mask1Collected = true;
                    else if (_maskSwitching.Mask1Collected && _maskSwitching.masksCollected == 1)
                        _maskSwitching.Mask2Collected = true;
                    else if (_maskSwitching.Mask2Collected && _maskSwitching.masksCollected == 2)
                        _maskSwitching.Mask3Collected = true;
                    else if (_maskSwitching.Mask3Collected && _maskSwitching.masksCollected == 3)
                        _maskSwitching.Mask4Collected = true;
                    else if (_maskSwitching.Mask4Collected && _maskSwitching.masksCollected == 4)
                        _maskSwitching.Mask5Collected = true;
                    _maskSwitching.masksCollected++;
                    hit.collider.gameObject.SetActive(false);
                }
        }
        public void PickupItem()
        {
            Ray ray = new(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out hit, interactRange))
                if (hit.collider.gameObject.CompareTag("Interactable") && _input.interact)
                {
                    collectablesCollected++;
                    hit.collider.gameObject.SetActive(false);
                }
        }
    }
}