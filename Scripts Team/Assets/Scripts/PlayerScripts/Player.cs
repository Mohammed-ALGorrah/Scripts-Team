using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Heros.Players
{
    public class Player : MonoBehaviourPunCallbacks
    {
        int id;
        Animator animator;
        public PlayerData playerData;
        HealthSystem health;
        ChargeSystem chargeSystem;
        GameManager gameManager;

        public bool CanSpecialAttack;
        public ParticleSystem fxSpecialAttack;
        public int numOfKills;
        public int numOfDead;

        [SerializeField]
        Slider healthBar;

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
            chargeSystem.OnChargeMaxed += Charge_Max_Event;
            health.OnDead += Deee;

            health.OnTakeDamage += Health_OnTakeDamage;
        }
        private void OnDisable()
        {
            chargeSystem.OnChargeMaxed -= Charge_Max_Event;
            health.OnDead -= Deee;
        }

        private void Charge_Max_Event(ChargeSystem obj)
        {
            GetComponent<PhotonView>().RPC("Charge_Max", RpcTarget.AllBuffered);
        }
        [PunRPC]
        private void Charge_Max()
        {
            fxSpecialAttack.gameObject.SetActive(true);
            CanSpecialAttack = true;
        }

        private void Health_OnTakeDamage(HealthSystem obj)
        {
            healthBar.value = health.currentHealth / health.maxHealth;
        }

        private void Deee(HealthSystem obj)
        {
            GetComponent<PhotonView>().RPC("Health_OnDead", RpcTarget.AllBuffered);
        }

        [PunRPC]
        private void Health_OnDead()
        {
            animator.SetTrigger("isDead");
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<PlayerAttack>().enabled = false;
            gameObject.GetComponent<PlayerMove>().enabled = false;
            gameObject.GetComponent<ChargeSystem>().enabled = false;
            gameObject.GetComponent<HealthSystem>().enabled = false;
            Destroy(Instantiate(Resources.Load("FireDeath"), new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity), 2f);
            Invoke("hidePlayer2", 1.5f);

            
        }
        [PunRPC]
        void hidePlayer()
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            gameObject.GetComponent<PlayerAttack>().enabled = true;
            gameObject.GetComponent<PlayerMove>().enabled = true;
            gameObject.GetComponent<ChargeSystem>().enabled = true;
            gameObject.GetComponent<HealthSystem>().enabled = true;

        }

        void hidePlayer2()
        {
            GetComponent<PhotonView>().RPC("hidePlayer", RpcTarget.AllBuffered);
        }

            public void HandleOnDeadth()
        {

        }

        public void ResponePlayer()
        {

        }

        private void OnTriggerEnter(Collider coll)
        {

            if (gameObject != coll.gameObject)
            {
                if (coll.gameObject.GetComponent<BulletManager>() != null)
                {
                    SkillData Sd = coll.gameObject.GetComponent<BulletManager>().skillData;
                    
                    if (Sd.skillType.ToString().Equals("NORMAL"))
                    {
                        
                        if (CheckFriend(Sd.player))
                        {
                            Debug.Log("friend");
                            return;
                        }
                        Debug.Log("normal dmg");
                        health.TakeDamage(Sd.skillDmg);
                        chargeSystem.IncreaseCharge(+1);

                        Destroy(Instantiate(Sd.hitEffect, coll.transform.position, Quaternion.identity), 2);
                        Destroy(coll.gameObject);

                    }
                    else if(Sd.skillType.ToString().Equals("SPECIAL"))
                    {
                        if (Sd.HelaingSkill)
                        {
                            if (CheckFriend(Sd.player))
                            {
                                Debug.Log("Helaing");
                                health.Heal(Sd.skillDmg);
                            }

                        }else{
                            if (CheckFriend(Sd.player))
                                {
                                    Debug.Log("Friend");
                                    return;
                                }
                                Debug.Log("special");
                                health.TakeDamage(Sd.skillDmg);
                                chargeSystem.IncreaseCharge(+2);
                                if (Sd.skillName == "Spin Dash")
                                {
                                    coll.gameObject.SetActive(false);
                                }
                        }
                    }
                }
            }
        }

        bool CheckFriend(Player player)
        {
            if ((gameManager.FirstTeam.players.Contains(player) && gameManager.FirstTeam.players.Contains(this)) ||
                 (gameManager.SecondTeam.players.Contains(player) && gameManager.SecondTeam.players.Contains(this)))
            {
                return true;
            }
            return false;

        }
    }

}
