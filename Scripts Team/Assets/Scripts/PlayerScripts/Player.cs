using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public PlayerData playerData;
    HealthSystem health;
    ChargeSystem chargeSystem;
    
    public bool CanSpecialAttack;
    public int numOfKills;
    public int numOfDead;

    private void Awake()
    {
        health = GetComponent<HealthSystem>();
        chargeSystem = GetComponent<ChargeSystem>();
    }
    void Start()
    {
        health.maxHealth = playerData.maxHealth;
        health.currentHealth = playerData.maxHealth;
        chargeSystem.maxCharage = playerData.maxCharge;
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

                SkillData Sd = coll.gameObject.GetComponent<BulletManager>().skillData;
                health.TakeDamage(Sd.skillDmg);

                Destroy(coll.gameObject);

            }else if(coll.gameObject.CompareTag("sward")){
                SkillData Sd = GetComponent<PlayerAttack>().basicAttack;
                health.TakeDamage(Sd.skillDmg);
                    
            }
        }
    }
}
