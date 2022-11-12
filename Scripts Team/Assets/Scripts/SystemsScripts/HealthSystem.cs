using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// namespace Scripts.SystemsScripts
// {
    public class HealthSystem : MonoBehaviour
    {
        public int currentHealth = 100;
        public int maxHealth = 100;
        //bool isDead;
        public event Action <HealthSystem> OnTakeDamage;
        public event Action <HealthSystem> OnDead;
        public event Action <HealthSystem> OnHeal;

        public void Start()
        {
            currentHealth = maxHealth;
        }

        public HealthSystem(int max)
        {
            maxHealth = max;
        }

        public void TakeDamage(int amount)
        {
            if (IsDead())
            {
                return;
            }
            if (currentHealth > 0 )
            {
                currentHealth -= amount;
                this.OnTakeDamage?.Invoke(this);

                if (currentHealth <= 0)
                {
                    this.OnDead.Invoke(this);
                }
            }
            //validate
            //OnTakeDamage?.Invoke(this);

            // amount <= 0
            //OnDead?.Invoke(this);
            //Dead();
        }


        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }
        /*void Dead()
        {
            isDead = true;
        }*/

    }
// }
