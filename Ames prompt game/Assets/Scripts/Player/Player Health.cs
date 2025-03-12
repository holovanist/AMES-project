using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float health = 2;
        float MaxHealth;
        public bool firstAttack = true;
        public float time;
        public float timer;
        public float ImunityTime = 0.25f;
        public Image healthbar;
        public float RegenAmount;
        public float RegenDelay;
        public int damageTaken = 2;
        // Start is called before the first frame update
        void Start()
        {
            MaxHealth = health;
            if(healthbar != null ) healthbar.fillAmount = health / MaxHealth;
            firstAttack = true;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            time -= Time.deltaTime;
            timer -= Time.deltaTime;
            if(timer <= 0 && health != MaxHealth)
            {
                health += RegenAmount;
                if (healthbar != null) healthbar.fillAmount = health / MaxHealth;
                if (timer <= 0) timer = RegenDelay;
            }
            if (health > MaxHealth)
            {
                health = MaxHealth;
            }
            if (time <= -3 && !firstAttack) time = ImunityTime;
            if (timer <= -3) timer = RegenDelay;
        }
        public void TakeDamage(int damage)
        {
            if (time <= 0)
            {
                health -= damage;
                if (healthbar != null) healthbar.fillAmount = health / MaxHealth;
                if (health <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                time = ImunityTime;
            }
        }
        private void OnTriggerStay(Collider coll)
        {
            
            if (coll.gameObject.CompareTag("Enemy"))
            {
                    TakeDamage(damageTaken);
                firstAttack = false;
            }
            if (coll.gameObject.CompareTag("Bullet"))
            {
                TakeDamage(damageTaken);
                firstAttack = false;
            }
        }
        private void OnTriggerEnter(Collider coll)
        {
            if (coll.gameObject.CompareTag("Enemy"))
            {
                TakeDamage(damageTaken);
                firstAttack = false;
            }
            if (coll.gameObject.CompareTag("Bullet"))
            {
                TakeDamage(damageTaken);
                firstAttack = false;
            }
        }
    }
}
