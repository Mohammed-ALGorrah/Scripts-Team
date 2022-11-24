using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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
        [SerializeField]
        Slider healthBar;

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
            //health.OnDead += Health_OnDead;
            //health.OnTakeDamage += Health_OnTakeDamage;
        }
        private void OnDisable()
        {
            chargeSystem.OnChargeMaxed -= Charge_Max;
            //health.OnDead -= Health_OnDead;
        }

        private void Charge_Max(ChargeSystem obj)
        {
            fxSpecialAttack.gameObject.SetActive(true);
            CanSpecialAttack = true;
        }
        
        [PunRPC]
        private void Health_OnTakeDamage(int m)
        {
            healthBar.value = health.currentHealth / health.maxHealth;
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
            //Invoke("hidePlayer", 1.5f);
        }

        void hidePlayer()
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
            gameObject.GetComponent<PlayerAttack>().enabled = true;
            gameObject.GetComponent<PlayerMove>().enabled = true;
            gameObject.GetComponent<ChargeSystem>().enabled = true;
            gameObject.GetComponent<HealthSystem>().enabled = true;

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
                if (coll.gameObject.GetComponent<BulletManager>() != null && !coll.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    SkillData Sd = coll.gameObject.GetComponent<BulletManager>().skillData;

                    if (Sd.skillType.ToString().Equals("NORMAL"))
                    {
                        if (CheckFriend(Sd.player))
                        {
                            Debug.Log("friend");
                            return;
                        }

                        //chargeSystem.IncreaseCharge(+1);

                        GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.AllBuffered,Sd.skillDmg,Sd.hitEffect.name);
                        //   PhotonNetwork.Destroy(coll.gameObject.GetComponent<PhotonView>());
                        

                    }
                    else
                    {
                        if (Sd.HelaingSkill)
                        {
                            if (CheckFriend(Sd.player))
                            {
                                Debug.Log("HelaingSkill");
                                health.Heal(Sd.skillDmg);
                            }

                        }
                        else
                        {
                            if (CheckFriend(Sd.player))
                            {
                                return;
                            }
                            //health.TakeDamage(Sd,coll.transform);
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
