using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heros.Players;

public class HealthSystem : MonoBehaviour
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

    /* public HealthSystem(int max)
     {
         maxHealth = max;
     }*/

    public void TakeDamage(int amount,Player playerKill = null,Player playerDead = null)
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
                if (playerKill != null && playerDead)
                {
                    playerKill.numOfKills++;
                    playerDead.numOfDead++;
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
