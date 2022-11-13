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

    public void HandleOnDeadth(){

    }

    public void ResponePlayer(){

    }


}
