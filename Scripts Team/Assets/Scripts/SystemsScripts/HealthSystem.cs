using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heros.Players;
using Photon.Pun;
public class HealthSystem : MonoBehaviourPunCallbacks
{
    public float currentHealth;
    public float maxHealth;

    public event Action<HealthSystem> OnTakeDamage;
    public event Action<HealthSystem> OnDead;
    public event Action<HealthSystem> OnHeal;

    Player playerKill;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    /* public HealthSystem(int max)
     {
         maxHealth = max;
     }*/

    [PunRPC]
    private void AddKillNum(int id)
    {
        //  foreach (KeyValuePair<int, Photon.Realtime.Player> kvp in PhotonNetwork.CurrentRoom.Players)
        //{
        //     Debug.Log("IDDDDDDDwww :" + kvp.Value.ActorNumber);
        //   }


            Debug.Log("IDDDDDDDwww :" + id);
            Debug.Log("IDDDDDDD www:" + GetComponent<PhotonView>().ViewID);


            
            playerKill.GetComponent<Player>().numOfKills++;
            Debug.Log("TXXXXXXXXXXXXeam :" + id + ": ");
        
        
    }


    public void TakeDamage(int amount, int attackID = 0 , Player p = null)
    {
        if (IsDead())
        {
            return;
        }
        if (currentHealth > 0)
        {
            currentHealth -= amount;

            this.OnTakeDamage?.Invoke(this);

            if (currentHealth <= 0)
            {
                this.OnDead?.Invoke(this);
                if (attackID != 0)
                {
                    //Debug.Log("IDDDDDDD :" + attackID);
                    //Debug.Log("IDDDDDDD :" + GetComponent<PhotonView>().ViewID);
                    p.GetComponent<Player>().icreasse(attackID);
                    //p.GetComponent<PhotonView>().RPC("AddKillNum", RpcTarget.AllBuffered, attackID);
                }
            }
        }

    }

    [PunRPC]
    private void AA()
    {

    }

    public void Heal(int amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
            this.OnHeal?.Invoke(this);
        }
        else
        {
            currentHealth += amount;
            this.OnHeal?.Invoke(this);
        }

    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

}
