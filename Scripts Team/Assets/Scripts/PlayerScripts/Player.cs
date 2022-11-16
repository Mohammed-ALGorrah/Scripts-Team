using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int id;
    public PlayerData playerData;
    HealthSystem health;
    ChargeSystem chargeSystem;
    
    public bool CanSpecialAttack;
    public int numOfKills;
    public int numOfDead;

    private void Awake()
    {
        id = Random.Range(100,1000000);

        health = GetComponent<HealthSystem>();
        chargeSystem = GetComponent<ChargeSystem>();
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
        gameObject.SetActive(false);
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
                if (Sd.skillType.ToString().Equals("NORMAL")) {          
                    health.TakeDamage(Sd.skillDmg);
                    chargeSystem.IncreaseCharge(+1);
                    Destroy(coll.gameObject);
                }
                else if(CanSpecialAttack)
                {
                    if (Sd.HelaingSkill)
                    {
                        health.Heal(Sd.skillDmg);
                        CanSpecialAttack = false;
                        chargeSystem.ResetCharge();
                    }
                    else
                    {
                        health.TakeDamage(Sd.skillDmg);
                        CanSpecialAttack = false;
                        chargeSystem.ResetCharge();
                        //Destroy(coll.gameObject);
                    }
                    
                }
                

            }else if(coll.gameObject.CompareTag("sward")){
                SkillData Sd = GetComponent<PlayerAttack>().basicAttack;
                health.TakeDamage(Sd.skillDmg);
                    
            }
        }
    }
}
