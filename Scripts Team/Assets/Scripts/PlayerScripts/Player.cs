using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public PlayerData playerData;
    HealthSystem health;
    ChargeSystem chargeSystem;

    [SerializeField]
    Transform BulletSpwanPoint;
    public bool CanSpecialAttack;
    public int numOfKills;
    public int numOfDead;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void HandleOnDeadth(){

    }

    public void ResponePlayer(){

    }


}
