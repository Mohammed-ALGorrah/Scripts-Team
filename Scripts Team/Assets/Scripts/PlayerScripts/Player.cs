using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Heros.Players
{
    public class Player : MonoBehaviour
    {
        int id;
        Animator animator;
        public PlayerData playerData;
        public HealthSystem health;
        public ChargeSystem chargeSystem;
        GameManager gameManager;

        public bool CanSpecialAttack;
        public ParticleSystem fxSpecialAttack;
        public int numOfKills;
        public int numOfDead;

        public GameObject diePanel;

        

        private void Awake()
        {
            id = Random.Range(100, 1000000);
            animator = GetComponent<Animator>();
            health = GetComponent<HealthSystem>();
            chargeSystem = GetComponent<ChargeSystem>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        void Start()
        {
            health.maxHealth = playerData.maxHealth;
            health.currentHealth = playerData.maxHealth;
            chargeSystem.maxCharage = playerData.maxCharge;
        }


        private void OnEnable()
        {
            chargeSystem.OnChargeMaxed += Charge_Max;
            health.OnDead += Health_OnDead;
        }
        private void OnDisable()
        {
            chargeSystem.OnChargeMaxed -= Charge_Max;
            health.OnDead -= Health_OnDead;
        }

        private void Charge_Max(ChargeSystem obj)
        {
            GetComponent<PhotonView>().RPC("Charge_Max_Pun", RpcTarget.AllBuffered);
        }
        [PunRPC]
        private void Charge_Max_Pun()
        {
            fxSpecialAttack.gameObject.SetActive(true);
            CanSpecialAttack = true;
        }
        private void Health_OnDead(HealthSystem obj)
        {
            GetComponent<PhotonView>().RPC("Health_OnDead_Pun", RpcTarget.AllBuffered);
            diePanel.SetActive(true);
        }

        [PunRPC]
        private void Health_OnDead_Pun()
        {
            animator.SetTrigger("isDead");            
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;  
            gameObject.GetComponent<PlayerAttack>().enabled = false;
            gameObject.GetComponent<PlayerMove>().enabled = false;
            gameObject.GetComponent<ChargeSystem>().enabled = false;
            gameObject.GetComponent<HealthSystem>().enabled = false;
            StartCoroutine("DestroyDieFx");
            StartCoroutine("ViewPlayer");         
        }

        IEnumerator DestroyDieFx()
        {
            GameObject DieFx = PhotonNetwork.Instantiate("Prefab/GameEffectsPrefabs/FireDeath", 
                new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            PhotonNetwork.Destroy(DieFx);
        }


        IEnumerator ViewPlayer()
        {
            yield return new WaitForSeconds(1.5f);
            GetComponentInParent<CheckPhoton>().GetComponent<PhotonView>().RPC("showPlayer", RpcTarget.All);
            gameObject.SetActive(false);
            
        }
        
    }
            

    }


