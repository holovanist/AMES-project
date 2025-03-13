using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Random = UnityEngine.Random;

namespace player
{
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class PlayerShoot : MonoBehaviour
    {
        //fix the shootDir
        [Header("Bullet settings")]
        public float ShootForce;
        public float UpwardForce, AbilityForce;
        [Header("Gun Stats")]
        public float TimeBetweenShooting;
        public float Spread, ReloadTime, TimeBetweenShots;
        public int MagSize, BulletsPerTap;
        public int BulletsLeft  {  get; set; }
        public int BulletsAvalible;
        int BulletsShot;
        bool Shooting, ReadyToShoot; 
        public bool Reloading {  get; set; }
        public bool AutoReload = true;

        [Header("Ability Stats")]
        public float TimeBetweenAbilities;
        public float saveCoolDown;
        public int Ability1Bullets;
        bool /*Ability1Active,*/ ReadyToActivate; 
        public bool SaveCoolDownActive {  get; set; }

        [Header("Referance Objects")]
        public Camera Cam;
        public Transform AttackPoint;
        public TextMeshProUGUI CoolDownCounter;

        [Header("Debuging")]
        public bool AllowInvoke = true;
        public bool AllowInvokeAbility = true;
        public bool animationActive;
        public bool animationActiveAbility;
        private StarterAssetsInputs _input;
        Animator anim;

        [Header("Graphics")]
        public GameObject MuzzleFlash;
        public TextMeshProUGUI AmmoDisplay;

        public void Awake()
        {
            BulletsLeft = MagSize;
            ReadyToShoot = true;
            ReadyToActivate = true;
        }
        void Start()
        {
            anim = GameObject.FindGameObjectWithTag("right").GetComponent<Animator>();
            saveCoolDown = TimeBetweenAbilities;

            _input = GetComponent<StarterAssetsInputs>();
        }

        void Update()
        {
            CoolDownCounter.SetText(String.Format("{0:0.00}", saveCoolDown));
            MyInput();
            if (AmmoDisplay != null)
                AmmoDisplay.SetText(BulletsLeft / BulletsPerTap + " / " + BulletsAvalible / BulletsPerTap);
            if(SaveCoolDownActive)
            {
                saveCoolDown -= Time.deltaTime;
            }

        }
        private void MyInput()
        {
            if (_input.Reload /*&& bulletsLeft < MagSize*/ && !Reloading) Reload();
            if (AutoReload)
            {
                if (ReadyToShoot && Shooting && !Reloading && BulletsLeft <= 0) Reload();
            }


            Shooting = _input.shoot;
            if (ReadyToShoot && Shooting && !Reloading && BulletsLeft > 0)
            {
                BulletsShot = 0;
                Shoot();
            }
            //Ability1Active = _input.Ability1;
            /*if (ReadyToActivate && Ability1Active && !Reloading && BulletsLeft > Ability1Bullets)
            {
                BulletsShot = 0;
                Ability1();
            }*/
        }
        private void Shoot()
        {
            ReadyToShoot = false;
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out RaycastHit hit)) targetPoint = hit.point;
            else targetPoint = ray.GetPoint(75);

            Vector3 directionWithoutSpread = targetPoint - AttackPoint.position;

            float x = Random.Range(-Spread, Spread);
            float y = Random.Range(-Spread, Spread);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            GameObject currentBullet = ObjectPooling.SharedInstance.GetPooledObject();
            if (currentBullet != null)
            {
                currentBullet.transform.SetPositionAndRotation(AttackPoint.transform.position, AttackPoint.transform.rotation);
                //currentBullet.SetActive(true);
                currentBullet.GetComponent<MeshRenderer>().enabled = true;
                currentBullet.GetComponent<SphereCollider>().enabled = true;

                currentBullet.transform.forward = directionWithSpread.normalized;
            }
            currentBullet.transform.forward = directionWithSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * ShootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(Cam.transform.up * UpwardForce, ForceMode.Impulse);

            if (MuzzleFlash != null)
                Instantiate(MuzzleFlash, AttackPoint.position, Quaternion.identity);

            BulletsLeft--;
            BulletsShot++;
            if (AllowInvoke)
            {
                //Invoke("ResetShot", 3f); calls function after 3 seconds
                Invoke(nameof(ResetShot), TimeBetweenShooting);
                AllowInvoke = false;
            }
            if (BulletsShot < BulletsPerTap && BulletsLeft > 0)
                Invoke(nameof(Shoot), TimeBetweenShots);
            if (anim != null && !animationActive)
            {
                anim.SetTrigger("shoot"); 
                animationActive = true;
            }
        }
        private void Ability1()
        {
            ReadyToActivate = false;
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out RaycastHit hit)) targetPoint = hit.point;
            else targetPoint = ray.GetPoint(75);

            Vector3 directionWithoutSpread = targetPoint - AttackPoint.position;

            float x = Random.Range(-Spread, Spread);
            float y = Random.Range(-Spread, Spread);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            GameObject currentBullet = ObjectPooling.SharedInstance.GetPooledObject();
            if (currentBullet != null)
            {
                currentBullet.transform.SetPositionAndRotation(AttackPoint.transform.position, AttackPoint.transform.rotation);
                //currentBullet.SetActive(true);
                currentBullet.GetComponent<MeshRenderer>().enabled = true;
                currentBullet.GetComponent<SphereCollider>().enabled = true;

                currentBullet.transform.forward = directionWithSpread.normalized;
            }
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * AbilityForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(Cam.transform.up * UpwardForce, ForceMode.Impulse);

            if (MuzzleFlash != null)
                Instantiate(MuzzleFlash, AttackPoint.position, Quaternion.identity);

            BulletsLeft--;
            BulletsShot++;
            if (anim != null && !animationActiveAbility)
            {
                anim.SetTrigger("ability1");
                animationActiveAbility = true;
            }
            if (AllowInvokeAbility)
            {
                if (saveCoolDown == TimeBetweenAbilities)
                {
                    //Invoke("ResetShot", 3f); calls function after 3 seconds
                    Invoke(nameof(ResetAbility), TimeBetweenAbilities);
                    AllowInvokeAbility = false;
                    SaveCoolDownActive = true;
                }
            }
            if (BulletsShot < Ability1Bullets && BulletsLeft > 0)
                Invoke(nameof(Ability1), TimeBetweenShots);
        }
        private void ResetAbility()
        {
            SaveCoolDownActive = false;
            saveCoolDown = TimeBetweenAbilities;
            ReadyToActivate = true;
            AllowInvokeAbility = true;
            animationActiveAbility = false;
        }
        private void ResetShot()
        {
            ReadyToShoot = true;
            AllowInvoke = true;
            animationActive = false;
        }
        private void Reload()
        {
            anim.SetTrigger("reload"); 
            Reloading = true;
            if(Reloading) Invoke(nameof(ReloadFinished), ReloadTime);
        }
        private void ReloadFinished()
        {
            int a = MagSize;
            int b = BulletsLeft;
            int c = BulletsAvalible;
            int d = b - a;
            int e = c - -d;
            if (BulletsAvalible >= MagSize)
            {
                BulletsLeft = MagSize;
                BulletsAvalible -= -d;
            } else
            {
                //get the billets left then only add an amout that will make it equal to at max MagSize
                BulletsLeft += -d;
                BulletsAvalible -= -d;
                if (e < 0) 
                {
                    BulletsAvalible += -e;
                    BulletsLeft += e; 
                }
            }
            Reloading = false;
        }
    }
}
