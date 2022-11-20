using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int id;
    Animator animator;
    public PlayerData playerData;
    HealthSystem health;
    ChargeSystem chargeSystem;
    GameManager gameManager;
        
    public bool CanSpecialAttack;
    public int numOfKills;
    public int numOfDead;

    private void Awake()
    {
        
        id = Random.Range(100,1000000);
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

    private void Charge_Max( ChargeSystem obj)
    {
        CanSpecialAttack = true;
    }
    private void Health_OnDead(HealthSystem obj)
    {   
        animator.SetTrigger("isDead");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<PlayerAttack>().enabled = false;
        gameObject.GetComponent<PlayerMove>().enabled = false;
        gameObject.GetComponent<ChargeSystem>().enabled = false;
        gameObject.GetComponent<HealthSystem>().enabled = false;
        Destroy(Instantiate(Resources.Load("FireDeath"),new Vector3(transform.position.x,transform.position.y+1,transform.position.z),Quaternion.identity),2f);
        Invoke("hidePlayer",1.5f);
    }

    void hidePlayer(){
        gameObject.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<PlayerAttack>().enabled = true;
        gameObject.GetComponent<PlayerMove>().enabled = true;
        gameObject.GetComponent<ChargeSystem>().enabled = true;
        gameObject.GetComponent<HealthSystem>().enabled = true;

    }

    void Update()
    {

    }

    void LateUpdate() {
        
    }

    public void HandleOnDeadth(){

    }

    public void ResponePlayer(){

    }


    private void OnTriggerEnter(Collider coll) {

        if(gameObject != coll.gameObject){
            if (coll.gameObject.GetComponent<BulletManager>() != null)
            {
                SkillData Sd= coll.gameObject.GetComponent<BulletManager>().skillData;

                if (Sd.skillType.ToString().Equals("NORMAL")) {  
                    if(CheckFriend(Sd.player)){
                             Debug.Log("friend");
                            return;
                    }
                    
                    health.TakeDamage(Sd.skillDmg);
                    chargeSystem.IncreaseCharge(+1);
                    
                    Destroy(Instantiate(Sd.hitEffect,coll.transform.position,Quaternion.identity),2);
                    Destroy(coll.gameObject);
                    
                }
                else 
                {
                    if (Sd.HelaingSkill)
                    {
                        if(CheckFriend(Sd.player)){
                             Debug.Log("HelaingSkill");
                            health.Heal(Sd.skillDmg);
                         }
                        
                    }
                    else
                    {
                        if(CheckFriend(Sd.player)){
                            return;
                         }
                        health.TakeDamage(Sd.skillDmg);
                        chargeSystem.IncreaseCharge(+2);
                    }
                    
                }
            }
        }
    }

    bool CheckFriend(Player player){
        if((gameManager.FirstTeam.players.Contains(player) && gameManager.FirstTeam.players.Contains(this)) ||
             (gameManager.SecondTeam.players.Contains(player) && gameManager.SecondTeam.players.Contains(this))){
            return true;
        }
        return false;
        
    }
}
