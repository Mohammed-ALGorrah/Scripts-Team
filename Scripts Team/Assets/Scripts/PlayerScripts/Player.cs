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
        health.OnDead += Health_OnDead;

    }
    private void OnDisable()
    {
        health.OnDead -= Health_OnDead;

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
                    Destroy(coll.gameObject);
                }
                else
                {
                    if (Sd.HelaingSkill)
                    {
                        health.Heal(Sd.skillDmg);
                    }
                    else
                    {
                        health.TakeDamage(Sd.skillDmg);
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
