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

    public void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int amount, int attackID = 0 , PlayerSetup p = null)
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
                     p.GetComponent<Player>().icreasse(attackID);
                }
            }
        }

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
