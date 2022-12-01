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
        }

        [PunRPC]
        private void Health_OnDead_Pun()
        {
            animator.SetTrigger("isDead");
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<PlayerAttack>().enabled = false;
            gameObject.GetComponent<PlayerMove>().enabled = false;
            gameObject.GetComponent<ChargeSystem>().enabled = false;
            gameObject.GetComponent<HealthSystem>().enabled = false;
            Destroy(Instantiate(Resources.Load("FireDeath"), new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity), 2f);
            Invoke("hidePlayer", 1.5f);            
        }
        void hidePlayer()
        {
            GetComponent<PhotonView>().RPC("hidePlayerPun", RpcTarget.AllBuffered);
        }
        [PunRPC]
        void hidePlayerPun()
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            gameObject.GetComponent<PlayerAttack>().enabled = true;
            gameObject.GetComponent<PlayerMove>().enabled = true;
            gameObject.GetComponent<ChargeSystem>().enabled = true;
            gameObject.GetComponent<HealthSystem>().enabled = true;
        }

        

        public bool CheckFriend(int player)
        {
            if ((gameManager.FirstTeam.players.Contains(player) && gameManager.FirstTeam.players.Contains(GetComponent<PhotonView>().ViewID)) ||
                 (gameManager.SecondTeam.players.Contains(player) && gameManager.SecondTeam.players.Contains(GetComponent<PhotonView>().ViewID)))
            {
                return true;
            }
            return false;

        }
    }

}
