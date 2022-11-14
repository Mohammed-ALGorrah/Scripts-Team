using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
    public class HealthSystem : MonoBehaviour
    {
        public int currentHealth;
        public int maxHealth ;

        public event Action <HealthSystem> OnTakeDamage;
        public event Action <HealthSystem> OnDead;
        public event Action <HealthSystem> OnHeal;

        public void Start()
        {
            currentHealth = maxHealth;
        }

       /* public HealthSystem(int max)
        {
            maxHealth = max;
        }*/

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
            
        }


        public void Heal(int amount)
        {
            if(currentHealth + amount > maxHealth){
                currentHealth = maxHealth;
            }else{
                currentHealth += amount;
            }
            
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }

    }
