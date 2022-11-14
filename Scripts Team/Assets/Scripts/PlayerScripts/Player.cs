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

    private void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.GetComponent<BulletManager>().skillData != null)
        {
            SkillData Sd = coll.gameObject.GetComponent<BulletManager>().skillData;
            health.TakeDamage(Sd.skillDmg);
            coll.gameObject.GetComponent<Rigidbody>().useGravity = false;


        }
    }

}
