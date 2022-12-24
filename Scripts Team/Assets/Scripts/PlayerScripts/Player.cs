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
        public HealthSystem health;
        public ChargeSystem chargeSystem;
        GameManager gameManager;

        public bool CanSpecialAttack;
        public ParticleSystem fxSpecialAttack;
        public int numOfKills;
        public int numOfDead;
        public Slider TopPowrBar;

        public GameObject diePanel;
        private void Awake()
        {
            TopPowrBar = FindObjectOfType<SyncTopChatgePowrBar>().GetComponent<Slider>();
            TopPowrBar.value = 0;
            id = Random.Range(100, 1000000);
            animator = GetComponent<Animator>();
            health = GetComponent<HealthSystem>();
            chargeSystem = GetComponent<ChargeSystem>();
        }
        void Start()
        {
            health.maxHealth = playerData.maxHealth;
            health.currentHealth = playerData.maxHealth;
            chargeSystem.maxCharage = playerData.maxCharge;
            
        }
     
        public void icreasse(int id)
        {
            if ((int)GetComponent<PhotonView>().Owner.CustomProperties["team"] == 1)
            {
                GameObject.Find("Teams").GetComponent<Team>().redAndBlueTeam.redTeam++;
            }
            else
            {
                GameObject.Find("Teams").GetComponent<Team>().redAndBlueTeam.blueTeam++;
            }
            numOfKills++;
            GetComponent<PhotonView>().RPC("addHitPlayer", RpcTarget.All, id);
        }

        [PunRPC]
        void addHitPlayer(int HitID)
        {
            if (GetComponent<PhotonView>().ViewID == id)
            {
                numOfKills++;
            }
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
            GetComponent<PhotonView>().RPC("Health_OnDead_Pun", RpcTarget.AllBuffered, photonView.ViewID, (int)PhotonNetwork.LocalPlayer.CustomProperties["team"]);
            diePanel.SetActive(true);
        }

        [PunRPC]
        private void Health_OnDead_Pun(int DieID, int team)
        {
            if (DieID == GetComponent<PhotonView>().ViewID)
            {
                numOfDead++;
                Debug.Log("Team Die:" + team + ": " + numOfDead);
            }


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


