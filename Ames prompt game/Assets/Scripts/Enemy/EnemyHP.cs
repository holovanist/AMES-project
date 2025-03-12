using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyHP : MonoBehaviour
    {
        public float health = 2;
        //float MaxHealth;
        float time;
        [SerializeField]
        float ImunityTime = 0.25f;
        //Image healthbar;
        //GameObject player;
        public bool EnemyDead, boss;
        public string LoadLevel;
        CapsuleCollider CC;
        SphereCollider SC;
        NavMeshAgent NMA;
        EnemyAi EA;
        MeshRenderer MR;
        SkinnedMeshRenderer[] SMR;
        Rigidbody rb;

        void Start()
        {
            CC = gameObject.GetComponent<CapsuleCollider>();
            SC = gameObject.GetComponent<SphereCollider>();
            NMA = gameObject.GetComponent<NavMeshAgent>();
            EA = gameObject.GetComponent<EnemyAi>();
            MR = gameObject.GetComponent<MeshRenderer>();
            SMR = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            rb = gameObject.GetComponent<Rigidbody>();


            //player = GameObject.FindGameObjectWithTag("Player");
            //MaxHealth = health;
            //healthbar = GetComponentsInChildren<Image>()[1];
            //healthbar.fillAmount = health / MaxHealth;

        }

        void Update()
        {
            time += Time.deltaTime;
            if (EnemyDead)
            {
                if (MR != null) MR.enabled = false;
                if (CC != null) CC.enabled = false;
                if (SC != null) SC.enabled = false;
                if (NMA != null) NMA.enabled = false;
                if (EA != null) EA.enabled = false;
                if(rb != null) rb.useGravity = false;
            }
            else
            {
                if (MR != null) MR.enabled = true;
                if (CC != null) CC.enabled = true;
                if(SC != null) SC.enabled = true;
                if (NMA != null) NMA.enabled = true;
                if (EA != null) EA.enabled = true;
                if (rb != null) rb.useGravity = true;
            }
            if (SMR != null)
            {
                SkinnedMeshRenderer[] meshRenderers = SMR;
                foreach (SkinnedMeshRenderer thisMeshRenderer in meshRenderers)
                {
                    if (EnemyDead)
                    {
                        thisMeshRenderer.enabled = false;
                    }
                    else
                    {
                        thisMeshRenderer.enabled = true;
                    }
                }
            }
        }
        public void TakeDamage(int damage)
        {
            if (time >= ImunityTime)
            {
                health -= damage;
                //healthbar.fillAmount = health / MaxHealth;
                if (health <= 0)
                {
                    if(boss)
                    {
                        SceneManager.LoadScene(LoadLevel);
                    }
                    //player.GetComponent<XpScript>().GiveXP(experianceAmount);
                    if(GetComponent<LootDropChance>() != null) GetComponent<LootDropChance>().InstantiateLoot(transform.position);
                    time = 0;
                    if (CC != null) CC.enabled = false;
                    if (SC != null) gameObject.GetComponent<SphereCollider>().enabled = false;
                    if (NMA != null) NMA.enabled = false;
                    if (EA != null) EA.enabled = false;
                    if (MR != null) MR.enabled = false;
                    if (rb != null) rb.useGravity = false;
                    EnemyDead = true;
                }
                time = 0;
            }
        }
    }
}