using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMovment
{
    public class PlayerCam : MonoBehaviour
    {
        public float sensX;
        public float sensY;
        public float MaxXRotationUp = 90f;
        public float MaxXRotationDown = -90f;

        public Transform orientatiion;

        float xRotation;
        float yRotation;
        private StarterAssetsInputs _input;
        public GameObject menu;
        float timer;
        float time;

        private void Awake()
        {
            time = .5f;
            menu = GameObject.FindGameObjectWithTag("menu");
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
            if (menu == null)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            if (menu != null)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            timer = time;
        }
        private void Update()
        {
            if (timer > 0)
                timer -= Time.deltaTime;

            float mouseX = -_input.look.x  * sensX;
            float mouseY = _input.look.y  * sensY;

            yRotation += mouseX;

            xRotation += mouseY;
            xRotation = Mathf.Clamp(xRotation, MaxXRotationDown, MaxXRotationUp);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientatiion.rotation = Quaternion.Euler(0, yRotation, 0);
            if(timer < 0 )
            {
                if (menu != null)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }

            }
        }
        private void OnLevelWasLoaded(int level)
        {
            timer = time;
            menu = GameObject.FindGameObjectWithTag("menu");
            if (menu == null)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (menu != null)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        private void OnApplicationFocus(bool focus)
        {
            if (menu == null)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if(menu != null)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }   
        }
    }
}